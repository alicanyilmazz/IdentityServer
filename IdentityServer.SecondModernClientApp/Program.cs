using IdentityServer.API2.Core.Repositories;
using IdentityServer.API2.Core.Services;
using IdentityServer.API2.Core.UnitOfWork;
using IdentityServer.API2.Data.Context;
using IdentityServer.API2.Data.Repositories.GenericRepositories;
using IdentityServer.API2.Data.UnitOfWork;
using IdentityServer.API2.Service.Services;
using IdentityServer.SharedLibrary.Common.Constants.InitializeSettings;
using IdentityServer.SharedLibrary.Configuration.TokenConfigurations;
using IdentityServer.SharedLibrary.Extensions.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>)); // CORE , DATA
builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>)); // CORE , SERVICE
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // CORE , DATA
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString(InitializeSetting.SQL_CONFIGURATION), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); 
    });
});
builder.Services.Configure<TokenConfiguration>(builder.Configuration.GetSection(InitializeSetting.TOKEN_CONFIGURATIONS));
builder.AddCustomAuthentication(tokenConfiguration: builder.Configuration.GetSection(InitializeSetting.TOKEN_CONFIGURATIONS).Get<TokenConfiguration>());
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
//{
//    option.Authority = "https://localhost:7025";
//    option.Audience = "resource_api2";
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
