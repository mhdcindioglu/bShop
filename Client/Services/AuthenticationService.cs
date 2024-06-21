using Blazored.LocalStorage;
using bShop.Client.Extensions;
using bShop.Client.Pages.Users;
using bShop.Shared.Extensions;
using bShop.Shared.Models;
using bShop.Shared.Models.Users;
using Microsoft.AspNetCore.Components.Authorization;

namespace bShop.Client.Services;

public class AuthenticationService(IUsersClient UsersSrv, AuthenticationStateProvider AuthenticationStateProv, ILocalStorageService LocalStorageSrv)
{
    public async Task<ApiResult<LoginResponseVM>> LoginAsync(LoginRequestVM model)
    {
        var response = await UsersSrv.LoginAsync(model);
        if (response.IsSuccess && response.Results != null)
        {
            await LocalStorageSrv.SetToken(response.Results);
            ((ShopAuthenticationStateProvider)AuthenticationStateProv).NotifyUserAuthentication(response.Results.Token);
        }

        return response;
    }
    public async Task<ApiResult<LoginResponseVM>> RegisterAsync(RegisterRequestVM model)
    {
        var response = await UsersSrv.RegisterAsync(model);
        if (response.IsSuccess && response.Results != null)
        {
            await LocalStorageSrv.SetToken(response.Results);
            ((ShopAuthenticationStateProvider)AuthenticationStateProv).NotifyUserAuthentication(response.Results.Token);
        }

        return response;
    }

    public async Task<ApiResult> LogoutAsync()
    {
        var response = await UsersSrv.LogoutAsync();
        if (response.IsSuccess)
        {
            await LocalStorageSrv.RemoveToken();
            ((ShopAuthenticationStateProvider)AuthenticationStateProv).NotifyUserLogout();
        }

        return response;
    }

    public async Task<ApiResult<LoginResponseVM>> RefreshTokenAsync(LoginResponseVM model)
    {
        var response = await UsersSrv.RefreshTokenAsync(model);
        if (response.IsSuccess && response.Results != null)
        {
            await LocalStorageSrv.SetToken(response.Results);
            ((ShopAuthenticationStateProvider)AuthenticationStateProv).NotifyUserAuthentication(response.Results.Token);
        }

        return response;
    }

    public async Task<ApiResult> ResetAsync(ResetRequestVM model) =>
         await UsersSrv.ResetAsync(model);

    public async Task<ApiResult> ResetPasswordAsync(ResetPasswordRequestVM model) =>
         await UsersSrv.ResetPasswordAsync(model);

    public async Task<ApiResult> ChangePasswordAsync(ChangePasswordRequestVM model) =>
         await UsersSrv.ChangePasswordAsync(model);

    public async Task<string?> GetAuthTokenAsync() =>
        await LocalStorageSrv.GetItemAsync<string?>(nameof(LoginResponseVM.Token));

    public async Task<string?> GetRefreshTokenAsync() =>
        await LocalStorageSrv.GetItemAsync<string?>(nameof(LoginResponseVM.RefreshToken));
    public async Task<DateTime?> GetAuthTokenExpireDateAsync() =>
        (await LocalStorageSrv.GetItemAsync<long?>(nameof(LoginResponseVM.TokenExpireDate))).FromTimeStamp();

    public async Task<DateTime?> GetRefreshTokenExpireDateAsync() =>
        (await LocalStorageSrv.GetItemAsync<long?>(nameof(LoginResponseVM.RefreshTokenExpireDate))).FromTimeStamp();
}
