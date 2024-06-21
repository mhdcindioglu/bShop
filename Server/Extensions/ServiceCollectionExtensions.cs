using bShop.Server.Data;
using bShop.Server.Data.Entities.Users;
using bShop.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace bShop.Server.Extensions;

public static class ServiceCollectionExtensions
{
    static IConfiguration Configuration = default!;
    public static IServiceCollection AddAppDbContext(this IServiceCollection services)
    {
        return services.AddDbContextFactory<ShopDB>((sp, opt) =>
        {
            opt.UseSqlServer(ShopDB.CS, x =>
            {
                x.MigrationsHistoryTable(ShopDB.MigrationsHistory, ShopDB.Schema);
                x.MigrationsAssembly("bShop.Server");
            });
        });
    }

    public static IServiceCollection AddAppIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        Configuration = configuration;
        services.AddIdentity<ShopUser, ShopRole>(ShopIdentityOptions).AddEntityFrameworkStores<ShopDB>().AddDefaultTokenProviders();
        services.AddAuthentication(ShopAuthenticationOptions).AddJwtBearer(ShopJwtBearerOptions);
        services.AddAuthorization(ShopAuthorizationOptions);
        return services;
    }

    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        return services;
    }

    private static void ShopIdentityOptions(IdentityOptions options)
    {
        options.SignIn.RequireConfirmedEmail = true;
        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
        options.Lockout.MaxFailedAccessAttempts = 5;
    }

    private static void ShopAuthenticationOptions(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    private static void ShopAuthorizationOptions(AuthorizationOptions options)
    {
        foreach (var prop in typeof(Roles).GetFields())
        {
            options.AddPolicy(prop.Name, policy => policy.RequireRole(prop.Name));
        }
    }

    private static void ShopJwtBearerOptions(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:Key").Value ?? throw new JwtKeyNotFoundException())),
        };
    }
}
