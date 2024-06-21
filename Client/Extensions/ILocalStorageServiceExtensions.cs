using Blazored.LocalStorage;
using bShop.Shared.Extensions;
using bShop.Shared.Models.Users;

namespace bShop.Client.Extensions;

public static class ILocalStorageServiceExtensions
{
    public static async Task SetToken(this ILocalStorageService localStorageSrv, LoginResponseVM model)
    {
        await localStorageSrv.SetItemAsStringAsync(nameof(LoginResponseVM.Token), model.Token);
        await localStorageSrv.SetItemAsStringAsync(nameof(LoginResponseVM.TokenExpireDate), model.TokenExpireDate.ToTimeStamp());
        await localStorageSrv.SetItemAsStringAsync(nameof(LoginResponseVM.RefreshToken), model.RefreshToken);
        await localStorageSrv.SetItemAsStringAsync(nameof(LoginResponseVM.RefreshTokenExpireDate), model.RefreshTokenExpireDate.ToTimeStamp());
    }

    public static async Task<LoginResponseVM> GetToken(this ILocalStorageService localStorageSrv) =>
        new LoginResponseVM()
        {
            Token = await localStorageSrv.GetItemAsStringAsync(nameof(LoginResponseVM.Token)) ?? "",
            TokenExpireDate = (await localStorageSrv.GetItemAsync<long?>(nameof(LoginResponseVM.TokenExpireDate))).FromTimeStamp(),
            RefreshToken = await localStorageSrv.GetItemAsStringAsync(nameof(LoginResponseVM.RefreshToken)) ?? "",
            RefreshTokenExpireDate = (await localStorageSrv.GetItemAsync<long?>(nameof(LoginResponseVM.RefreshTokenExpireDate))).FromTimeStamp(),
        };

    public static async Task<LoginResponseVM> GetToken(this ILocalStorageService localStorageSrv, CancellationToken cancellationToken) =>
        new LoginResponseVM()
        {
            Token = await localStorageSrv.GetItemAsStringAsync(nameof(LoginResponseVM.Token), cancellationToken) ?? "",
            TokenExpireDate = (await localStorageSrv.GetItemAsync<long?>(nameof(LoginResponseVM.TokenExpireDate), cancellationToken)).FromTimeStamp(),
            RefreshToken = await localStorageSrv.GetItemAsStringAsync(nameof(LoginResponseVM.RefreshToken), cancellationToken) ?? "",
            RefreshTokenExpireDate = (await localStorageSrv.GetItemAsync<long?>(nameof(LoginResponseVM.RefreshTokenExpireDate), cancellationToken)).FromTimeStamp(),
        };

    public static async Task RemoveToken(this ILocalStorageService localStorageSrv)
    {
        await localStorageSrv.RemoveItemAsync(nameof(LoginResponseVM.Token));
        await localStorageSrv.RemoveItemAsync(nameof(LoginResponseVM.TokenExpireDate));
        await localStorageSrv.RemoveItemAsync(nameof(LoginResponseVM.RefreshToken));
        await localStorageSrv.RemoveItemAsync(nameof(LoginResponseVM.RefreshTokenExpireDate));
    }

}
