using Microsoft.JSInterop;

namespace bShop.Data.Services;
public class CookieService(IJSRuntime JS)
{
    public async Task SetCookie(string name, string value, int days) =>
        await JS.InvokeVoidAsync("setCookie", name, value, days);

    public async Task<string> GetCookie(string name) =>
        await JS.InvokeAsync<string>("getCookie", name);

    public async Task DeleteCookie(string name) =>
        await JS.InvokeVoidAsync("deleteCookie", name);
}

