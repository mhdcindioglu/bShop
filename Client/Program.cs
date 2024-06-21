using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using bShop.Client;
using bShop.Client.Extensions;
using bShop.Client.Handlers;
using bShop.Client.Pages;
using bShop.Client.Pages.Users;
using bShop.Client.Services;
using bShop.Localization;
using bShop.Shared.Models.Languages;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ShopHttpMessageHandler>();
builder.Services.AddHttpClient("bShop.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("bShop.ServerAPI"));

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<ShopAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ShopAuthenticationStateProvider>());

builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));

builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddFluentUIComponents();
builder.Services.AddAuthorizationCore();

builder.Services.AddAppRefitClient<IUsersClient>(new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddAppRefitClient<ILanguagesClient>(new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddLocalization();

var app = builder.Build();

CultureInfo culture;
var LocalStorageSrv = app.Services.GetRequiredService<ILocalStorageService>();
var lang = await LocalStorageSrv.GetItemAsync<LanguageVM>("Lang");

if (lang != null)
    culture = new CultureInfo(lang.Culture);
else
{
    lang = new LanguageVM { Name = "English", Culture = "en-US", SeoCode = "en", Rtl = false, };
    culture = new CultureInfo(lang.Culture);
    await LocalStorageSrv.SetItemAsync("Lang", lang);
}

CultureInfo.CurrentCulture = culture;
CultureInfo.CurrentUICulture = culture;
Resources.Culture = culture;

await app.RunAsync();
