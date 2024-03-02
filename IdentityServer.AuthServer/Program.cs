using IdentityServer.AuthServer.Models;
using IdentityServer.AuthServer.Repository;
using IdentityServer.AuthServer.Services;
using IdentityServer.SharedLibrary.Common.Constants.InitializeSettings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICustomUserRepository, CustomUserRepository>();
builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(IdentityServer.AuthServer.Config.GetApiResources())
    .AddInMemoryApiScopes(IdentityServer.AuthServer.Config.GetApiScopes())
    .AddInMemoryClients(IdentityServer.AuthServer.Config.GetClients())
    .AddInMemoryIdentityResources(IdentityServer.AuthServer.Config.GetIdentityResources())
    //.AddTestUsers(IdentityServer.AuthServer.Config.GetUsers().ToList())
    .AddProfileService<CustomProfileService>()
    .AddResourceOwnerValidator<ResorceOwnerPasswordValidator>()
    .AddDeveloperSigningCredential();
builder.Services.AddDbContext<CustomDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString(InitializeSetting.SQL_CONFIGURATION), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(CustomDbContext)).GetName().Name);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
