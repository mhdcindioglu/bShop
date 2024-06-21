using bShop.Client.Helpers;
using bShop.Server.Data.Entities.Users;
using bShop.Server.Services;
using bShop.Shared.Models;
using bShop.Shared.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

namespace bShop.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserManager<ShopUser> UserManager, SignInManager<ShopUser> SignInManager, IConfiguration Configuration, IEmailSender EmailSender) : ControllerBase
{
    private readonly TimeSpan TokenExpiredAfter = TimeSpan.FromSeconds(20);
    private readonly TimeSpan RefreshTokenExpiredAfter = TimeSpan.FromDays(7);

    [HttpPost("Login")]
    public async Task<ApiResult<LoginResponseVM>> Login(LoginRequestVM model)
    {
        var response = new ApiResult<LoginResponseVM>();

        try
        {
            if (!ModelState.IsValid)
                response.AddError(ApiResultErrorType.ModelStateIsNotValid);
            else
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                if (user == null || user.IsDeleted)
                    response.AddError(ApiResultErrorType.UnAuthorized);
                else if (!user.IsActive)
                    response.AddError(ApiResultErrorType.UserNotActive);
                else
                {
                    var result = await SignInManager.CheckPasswordSignInAsync(user, model.Password, true);
                    if (result.Succeeded)
                        response.Results = await SigninUser(user);
                    else if (result.IsLockedOut)
                        response.AddError(ApiResultErrorType.UserLocked);
                    else if (result.IsNotAllowed)
                        response.AddError(ApiResultErrorType.UserNeedEmailActivation);
                    else if (result.RequiresTwoFactor)
                        response.AddError(ApiResultErrorType.UserRequiresTwoFactor);
                    else
                        response.AddError(ApiResultErrorType.UnAuthorized);
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerMessage());
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [HttpGet("Logout")]
    public async Task<ApiResult> Logout()
    {
        var response = new ApiResult();

        try
        {
            if (User.Identity?.Name != null)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    user.RefreshToken = null;
                    user.RefreshTokenExpiry = null;
                    await UserManager.UpdateAsync(user);
                }
            }

            await SignInManager.SignOutAsync();
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerMessage());
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [HttpPost("RefreshToken")]
    public async Task<ApiResult<LoginResponseVM>> RefreshToken(LoginResponseVM model)
    {
        var response = new ApiResult<LoginResponseVM>() { Results = new(), };

        try
        {
            var jwtKey = Configuration.GetSection("Jwt:Key").Value ?? throw new JwtKeyNotFoundException();
            var principal = LoginHelpers.GetTokenPrincipal(model.Token, jwtKey);

            if (principal?.Identity?.Name is null)
                response.AddError(ApiResultErrorType.UnAuthorized);
            else
            {
                var user = await UserManager.FindByNameAsync(principal.Identity.Name);
                if (user == null || !user.IsActive || user.IsDeleted || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
                    response.AddError(ApiResultErrorType.UnAuthorized);
                else
                    response.Results = await SigninUser(user);
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerMessage());
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [HttpPost("Register")]
    public async Task<ApiResult<LoginResponseVM>> Register(RegisterRequestVM model)
    {
        var response = new ApiResult<LoginResponseVM>();

        if (!ModelState.IsValid)
            response.AddError(ApiResultErrorType.ModelStateIsNotValid);
        else
        {
            try
            {
                var newUser = model.Map<ShopUser>();
                newUser.Email = model.UserName;
                newUser.UserName = newUser.Email;
                newUser.PasswordUpdated = true;
                var result = await UserManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByEmailAsync(newUser.Email!);
                    if (user != null)
                        await SendEmailConfirmation(user);
                }
                else
                    foreach (var err in result.Errors)
                    {
                        if (err.Code == "DuplicateUserName")
                            response.AddError(ApiResultErrorType.AddedBefore, Resources.UserEmailAddedBefore, nameof(RegisterRequestVM.UserName));
                        else
                            response.AddError(err.Description);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerMessage());
                response.AddError(ex.InnerMessage());
            }
        }

        return response;
    }

    [HttpPost("Reset")]
    public async Task<ApiResult> Reset(ResetRequestVM model)
    {
        var response = new ApiResult();

        try
        {
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user != null && user.IsActive && !user.IsDeleted)
            {
                var code = await UserManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = $"{Request.Scheme}://{Request.Host.Value}/ResetPassword/{user.Id}/{HttpUtility.UrlEncode(code)}";
                await EmailSender.SendPasswordResetLinkAsync(user, model.Email, callbackUrl);
            }
        }
        catch (Exception ex)
        {
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [HttpPost("ResetPassword")]
    public async Task<ApiResult> ResetPassword(ResetPasswordRequestVM model)
    {
        var response = new ApiResult();

        try
        {
            var userInDb = await UserManager.FindByIdAsync(model.ID.ToString());
            if (userInDb == null)
                response.AddError(ApiResultErrorType.InvalidLink);
            else
            {
                var result = await UserManager.ResetPasswordAsync(userInDb, model.Code, model.NewPassword);
                if (!result.Succeeded)
                {
                    if (result.Errors.Any(x => x.Code == "InvalidToken"))
                        response.AddError(ApiResultErrorType.InvalidLink);
                    else if (result.Errors.Select(x => x.Code).Any(x =>
                        x == "PasswordTooShort" ||
                        x == "PasswordRequiresDigit" ||
                        x == "PasswordRequiresLower" ||
                        x == "PasswordRequiresUpper" ||
                        x == "PasswordRequiresUniqueChars" ||
                        x == "PasswordRequiresNonAlphanumeric"))
                    {
                        foreach (var err in result.Errors.Select(x => x.Code))
                            response.AddError(ApiResultErrorType.InvalidModel, err, nameof(ResetPasswordRequestVM.NewPassword));
                    }
                }
                else
                {
                    var callbackUrl = $"{Request.Scheme}://{Request.Host.Value}/ReResetPassword/{HttpUtility.UrlEncode(userInDb.Email)}";
                    await EmailSender.SendPasswordResettedAsync(userInDb, callbackUrl);
                }
            }
        }
        catch (Exception ex)
        {
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [Authorize]
    [HttpPost("ChangePassword")]
    public async Task<ApiResult> ChangePassword(ChangePasswordRequestVM model)
    {
        var response = new ApiResult();

        try
        {
            var userInDb = await UserManager.FindByIdAsync(model.ID.ToString());
            if (userInDb == null)
                response.AddError(ApiResultErrorType.NotFound);
            else
            {
                var result = await UserManager.ChangePasswordAsync(userInDb, model.OldPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    if (result.Errors.Select(x => x.Code).Any(x => x == "PasswordMismatch"))
                        response.AddError(ApiResultErrorType.InvalidModel, "PasswordMismatch", nameof(ChangePasswordRequestVM.OldPassword));
                    else if (result.Errors.Select(x => x.Code).Any(x =>
                            x == "PasswordTooShort" ||
                            x == "PasswordRequiresDigit" ||
                            x == "PasswordRequiresLower" ||
                            x == "PasswordRequiresUpper" ||
                            x == "PasswordRequiresUniqueChars" ||
                            x == "PasswordRequiresNonAlphanumeric"))
                    {
                        foreach (var err in result.Errors.Select(x => x.Code))
                            response.AddError(ApiResultErrorType.InvalidModel, err, nameof(ChangePasswordRequestVM.NewPassword));
                    }
                }
                else
                {
                    var callbackUrl = $"{Request.Scheme}://{Request.Host.Value}/ReResetPassword/{HttpUtility.UrlEncode(userInDb.Email)}";
                    await EmailSender.SendPasswordChangedAsync(userInDb, callbackUrl);
                }
            }
        }
        catch (Exception ex)
        {
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [HttpPost("EmailConfirmation")]
    public async Task<ApiResult> EmailConfirmation(string user)
    {
        var response = new ApiResult();

        try
        {
            var userInDb = await UserManager.FindByEmailAsync(user);
            if (userInDb != null)
                await SendEmailConfirmation(userInDb);
        }
        catch (Exception ex)
        {
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string user, string code)
    {
        try
        {
            var userInDb = await UserManager.FindByIdAsync(user);
            if (userInDb != null)
            {
                var result = await UserManager.ConfirmEmailAsync(userInDb, code);
                if (!result.Succeeded)
                    foreach (var err in result.Errors.Select(x => new ApiResultError { Message = x.Description, }))
                        Console.WriteLine(err);
            }
            else
                await SignInManager.SignOutAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerMessage());
        }

        return Redirect("/EmailConfirmed");
    }

    [Authorize]
    [HttpPost("GetProfile")]
    public async Task<ApiResult<ProfileRequestVM>> GetProfile()
    {
        var response = new ApiResult<ProfileRequestVM>();

        try
        {
            var userInDb = await UserManager.FindByNameAsync(User.Identity!.Name!);
            if (userInDb != null)
                response.Results = userInDb.Map<ProfileRequestVM>();
            else
                response.AddError(ApiResultErrorType.NotFound);
        }
        catch (Exception ex)
        {
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    [Authorize]
    [HttpPost("UpdateProfile")]
    public async Task<ApiResult<LoginResponseVM>> UpdateProfile(ProfileRequestVM model)
    {
        var response = new ApiResult<LoginResponseVM>();

        try
        {
            var userInDb = await UserManager.FindByIdAsync(model.ID.ToString());
            if (userInDb == null)
                response.AddError(ApiResultErrorType.NotFound);
            else
            {
                userInDb.FullName = model.FullName.Trim();
                userInDb.PhoneNumber = model.PhoneNumber.Trim();
                userInDb.Address = model.Address?.Trim();
                await UserManager.UpdateAsync(userInDb);

                response.Results = await SigninUser(userInDb);
            }
        }
        catch (Exception ex)
        {
            response.AddError(ex.InnerMessage());
        }

        return response;
    }

    private async Task<LoginResponseVM> SigninUser(ShopUser user)
    {
        await SignInManager.SignInAsync(user, isPersistent: false);
        var jwtKey = Configuration.GetSection("Jwt:Key").Value ?? throw new JwtKeyNotFoundException();
        var newClaims = new List<Claim>
        {
            new("FullName", user.FullName),
            new("FirstName", user.FullName.Split(" ").FirstOrDefault() ?? ""),
            new("PhoneNumber", user.FullName),
        };

        var response = new LoginResponseVM
        {
            Token = LoginHelpers.GenerateTokenString(User, TokenExpiredAfter, jwtKey, newClaims),
            TokenExpireDate = DateTime.UtcNow.Add(TokenExpiredAfter),
            RefreshToken = LoginHelpers.GenerateRefreshTokenString(),
            RefreshTokenExpireDate = DateTime.UtcNow.Add(RefreshTokenExpiredAfter),
        };

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiry = response.RefreshTokenExpireDate;
        await UserManager.UpdateAsync(user);

        return response;
    }

    private async Task SendEmailConfirmation(ShopUser user)
    {
        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = $"{Request.Scheme}://{Request.Host.Value}/Api/Users/ConfirmEmail?user={user.Id}&code={HttpUtility.UrlEncode(code)}";
        await EmailSender.SendConfirmationLinkAsync(user, user.Email!, callbackUrl);
    }
}