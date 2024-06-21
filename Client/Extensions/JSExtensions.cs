using bShop.Client.Models;
using Microsoft.JSInterop;

namespace bShop.Client.Extensions;

public static class JSExtensions
{
    public static async Task MessageBox(this IJSRuntime js, string message, string? title = null, string icon = SweetAlert.Icons.Info) =>
        await js.InvokeVoidAsync("messageBox", message, title, icon.ToString());

    public static async Task<string> ConfirmationBox(this IJSRuntime js, string message, string? title = null, string[]? buttons = null, string[]? colors = null, string icon = SweetAlert.Icons.Question) =>
        await js.InvokeAsync<string>("confirmationBox", message, title, buttons ?? ["Yes", "No"], colors ?? [SweetAlert.Colors.Primary, SweetAlert.Colors.Danger, SweetAlert.Colors.Secondary], icon.ToString().ToLower());
}
