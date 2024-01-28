using IdentityServer.SharedLibrary.Configuration.CookieConfigurations;
using IdentityServer.SharedLibrary.Configuration.TokenConfigurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.SharedLibrary.Extensions.Authorization
{
    public static class AuthenticationHelper
    {
        /// <summary>
        /// This method adding Jwt Authentication Validation
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="tokenConfiguration"></param>
        public static void AddCustomAuthentication(this WebApplicationBuilder builder , TokenConfiguration tokenConfiguration)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
            {
                option.Authority = tokenConfiguration.Authority;
                option.Audience = tokenConfiguration.Audience.First();
            });
        }

        /// <summary>
        /// This method adding Cookie Authentication Validation
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="cookieConfiguration"></param>
        public static void AddCustomCookieAuthentication(this WebApplicationBuilder builder, CookieConfiguration cookieConfiguration)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = cookieConfiguration.DefaultScheme;
                options.DefaultChallengeScheme = cookieConfiguration.DefaultChallengeScheme;
            }).AddCookie(cookieConfiguration.DefaultScheme).AddOpenIdConnect(cookieConfiguration.DefaultChallengeScheme, oidcOptions =>
            {
                oidcOptions.SignInScheme = cookieConfiguration.DefaultScheme;
                oidcOptions.Authority = cookieConfiguration.Authority;
                oidcOptions.ClientId = cookieConfiguration.ClientId;
                oidcOptions.ClientSecret = cookieConfiguration.ClientSecret;
                oidcOptions.ResponseType = cookieConfiguration.ReturnType;
                oidcOptions.GetClaimsFromUserInfoEndpoint = true;
                oidcOptions.SaveTokens = true;
                oidcOptions.Scope.Add("api1.read");
                oidcOptions.Scope.Add("offline_access");
                oidcOptions.Scope.Add("CountryAndCity");
                oidcOptions.ClaimActions.MapUniqueJsonKey("country", "country");
                oidcOptions.ClaimActions.MapUniqueJsonKey("city", "city");
            });
        }
    }
}
