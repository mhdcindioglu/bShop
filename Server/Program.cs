using bShop.Server.Data;
using bShop.Server.Extensions;
using bShop.Server.Services;

var builder = WebApplication.CreateBuilder(args);


var cs = builder.Environment.IsDevelopment() ? CS.CS_Debug : CS.CS_Release;
ShopDB.CS = builder.Configuration.GetConnectionString(cs) ?? throw new ConnectionStringNotFoundException(cs);
builder.Services.AddAppDbContext();
builder.Services.AddAppIdentity(builder.Configuration);

builder.Services.AddAppSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseAppSwagger();
}
else
{
    app.UseHsts();
}

await app.UseAppDbMigration();

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToFile("index.html");

await app.RunAsync();
