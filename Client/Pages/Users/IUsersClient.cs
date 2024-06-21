using bShop.Shared.Extensions;
using bShop.Shared.Models;
using bShop.Shared.Models.Users;
using Refit;

namespace bShop.Client.Pages.Users;


public interface IUsersClient
{
    [Post("/Login")]
    Task<ApiResult<LoginResponseVM>> LoginAsync(LoginRequestVM model);
    
    [Get("/Logout")]
    Task<ApiResult> LogoutAsync();

    [Post("/RefreshToken")]
    Task<ApiResult<LoginResponseVM>> RefreshTokenAsync(LoginResponseVM model);

    [Post("/Register")]
    Task<ApiResult<LoginResponseVM>> RegisterAsync(RegisterRequestVM model);

    [Post("/Reset")]
    Task<ApiResult> ResetAsync(ResetRequestVM model);

    [Post("/ResetPassword")]
    Task<ApiResult> ResetPasswordAsync(ResetPasswordRequestVM model);

    [Post("/ChangePassword")]
    Task<ApiResult> ChangePasswordAsync(ChangePasswordRequestVM model);

    [Post("/EmailConfirmation")]
    Task<ApiResult> EmailConfirmation(string user);

    [Post("/GetProfile")]
    Task<ApiResult<ProfileRequestVM>> GetProfileAsync();

    [Post("/UpdateProfile")]
    Task<ApiResult<LoginResponseVM>> UpdateProfileAsync(ProfileRequestVM model);
}
