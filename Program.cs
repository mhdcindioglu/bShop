using Blazored.LocalStorage;
using Blazored.Toast;
using bShop.Components;
using bShop.Components.Account;
using bShop.Data;
using bShop.Data.Entities;
using bShop.Data.Services;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bShop
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("SqlServerUserCS") ?? throw new InvalidOperationException("Connection string 'SqlServerUserCS' not found.");;

            builder.Services.AddDbContextFactory<ShopContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();

            builder.Services.AddIdentityCore<ShopUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ShopContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));

            builder.Services.AddSingleton<IEmailSender<ShopUser>, IdentityNoOpEmailSender>();

            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<INewsLetterService, NewsLetterService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<CookieService>();

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.MapStaticAssets();
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
            app.MapAdditionalIdentityEndpoints();
            await app.RunAsync();
        }
    }
}
