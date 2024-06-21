using Blazored.LocalStorage;
using bShop.Client.Extensions;
using bShop.Client.Helpers;
using bShop.Client.Store.CurrentUserState;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace bShop.Client.Services;

public class ShopAuthenticationStateProvider(ILocalStorageService localStorageService, IDispatcher Dispatcher) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorageService.GetItemAsync<string>("Token");

        if (string.IsNullOrEmpty(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var identity = new ClaimsIdentity(LoginHelpers.ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity); 
        Dispatcher.Dispatch(new UpdateCurrentUserAction(user.GetCurrnetUserModel()));
        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(string token)
    {
        var identity = new ClaimsIdentity(LoginHelpers.ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);
        Dispatcher.Dispatch(new UpdateCurrentUserAction(user.GetCurrnetUserModel()));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        Dispatcher.Dispatch(new UpdateCurrentUserAction(user.GetCurrnetUserModel()));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}
