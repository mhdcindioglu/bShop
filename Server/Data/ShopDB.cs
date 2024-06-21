using bShop.Server.Data.Entities.Languages;
using bShop.Server.Data.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bShop.Server.Data;

public class ShopDB(DbContextOptions<ShopDB> options) : IdentityDbContext<ShopUser, ShopRole, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    public static string? CS { get; set; }
    public const string Schema = "Shop";
    public const string MigrationsHistory = "__MigrationsHistory";

    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(Schema);

        builder.Entity<ShopUser>().ToTable("Users");
        builder.Entity<ShopRole>().ToTable("Roles");
        builder.Entity<RoleClaim>().ToTable("RoleClaims");
        builder.Entity<UserRole>().ToTable("UserRoles");
        builder.Entity<UserClaim>().ToTable("UserClaims");
        builder.Entity<UserLogin>().ToTable("UserLogins");
        builder.Entity<UserToken>().ToTable("RoleTokens");

        //ShopUser
        builder.Entity<ShopUser>().HasMany(x => x.Roles).WithMany(x => x.Users).UsingEntity<UserRole>();

        //Language
        builder.Entity<Language>().HasIndex(x => x.Name).IsUnique();
        builder.Entity<Language>().HasIndex(x => x.SeoCode).IsUnique();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(CS, x =>
        {
            x.MigrationsHistoryTable(MigrationsHistory, Schema);
            x.MigrationsAssembly("bShop.Server");
        });
    }
}
