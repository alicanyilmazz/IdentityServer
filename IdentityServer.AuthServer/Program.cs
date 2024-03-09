using IdentityServer.AuthServer.Seeds;
using IdentityServer.SharedLibrary.Common.Constants.InitializeSettings;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString(InitializeSetting.SQL_CONFIGURATION), sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(assemblyName);
            //sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(ConfigurationDbContext)).GetName().Name);
        });

    }).AddOperationalStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString(InitializeSetting.SQL_CONFIGURATION), sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(assemblyName);
            //sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(PersistedGrantDbContext)).GetName().Name);
        });
    })
    //.AddInMemoryApiResources(IdentityServer.AuthServer.Config.GetApiResources())
    //.AddInMemoryApiScopes(IdentityServer.AuthServer.Config.GetApiScopes())
    //.AddInMemoryClients(IdentityServer.AuthServer.Config.GetClients())
    //.AddInMemoryIdentityResources(IdentityServer.AuthServer.Config.GetIdentityResources())
    //.AddTestUsers(IdentityServer.AuthServer.Config.GetUsers().ToList())
    .AddDeveloperSigningCredential();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<ConfigurationDbContext>();
    IdentityServerSeedData.Seeds(context);
}
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
