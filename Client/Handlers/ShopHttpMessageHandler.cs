using Blazored.LocalStorage;
using bShop.Client.Extensions;
using bShop.Client.Helpers;
using bShop.Client.Services;
using bShop.Client.Store.CurrentUserState;
using bShop.Shared.Models;
using bShop.Shared.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace bShop.Client.Handlers;

public class ShopHttpMessageHandler(ILocalStorageService LocalStorageSrv, HttpClient Http, AuthenticationStateProvider Auth, NavigationManager NavMngr) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await LocalStorageSrv.GetItemAsStringAsync(nameof(LoginResponseVM.Token), cancellationToken);

        if (token != null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Refresh the token
            var refreshToken = await LocalStorageSrv.GetItemAsStringAsync(nameof(LoginResponseVM.RefreshToken), cancellationToken);
            if (refreshToken == null)
                return response;

            var model = await LocalStorageSrv.GetToken(cancellationToken);
            var httpRespons = await Http.PostAsJsonAsync("/Api/Users/RefreshToken", model, cancellationToken);
            if (httpRespons.IsSuccessStatusCode)
            {
                var content = await httpRespons.Content.ReadAsStringAsync(cancellationToken);
                var refreshResponse = JsonSerializer.Deserialize<ApiResult<LoginResponseVM>>(content) ?? new();
                if (refreshResponse.IsSuccess && refreshResponse.Results != null)
                {
                    await LocalStorageSrv.SetToken(refreshResponse.Results);
                    await ((ShopAuthenticationStateProvider)Auth).GetAuthenticationStateAsync();
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshResponse.Results.Token);
                    response = await base.SendAsync(request, cancellationToken);
                }
                else 
                {
                    await Http.GetAsync("/Api/Users/Logout", cancellationToken);
                    NavMngr.NavigateTo("/Logout");
                }
            }
        }

        return response;
    }
}
