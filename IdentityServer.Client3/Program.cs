using IdentityServer.SharedLibrary.Common.Constants.InitializeSettings;
using IdentityServer.SharedLibrary.Configuration.CookieConfigurations;
using IdentityServer.SharedLibrary.Extensions.Authorization;
using IdentityServer.SharedLibrary.Extensions.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<BasicCookieConfiguration>(builder.Configuration.GetSection(InitializeSetting.COOKIE_CONFIGURATIONS));
builder.AddCustomCookieAuthenticationWithoutOpenIdConnect(cookieConfiguration: builder.Configuration.GetSection(InitializeSetting.COOKIE_CONFIGURATIONS).Get<BasicCookieConfiguration>());
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IApiResourcesHttpClient, ApiResourcesHttpClient>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
