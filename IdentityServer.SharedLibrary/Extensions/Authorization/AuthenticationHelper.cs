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
        /// <param name="cookieConfiguration">Provides Cookie customization properties</param>
        public static void AddCustomCookieAuthentication(this WebApplicationBuilder builder, CookieConfiguration cookieConfiguration)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = cookieConfiguration.DefaultScheme;
                options.DefaultChallengeScheme = cookieConfiguration.DefaultChallengeScheme;
            }).AddCookie(cookieConfiguration.DefaultScheme, options =>
            {
                options.AccessDeniedPath = cookieConfiguration.AccessDeniedPath;

            }).AddOpenIdConnect(cookieConfiguration.DefaultChallengeScheme, oidcOptions =>
            {
                oidcOptions.SignInScheme = cookieConfiguration.DefaultScheme;
                oidcOptions.Authority = cookieConfiguration.Authority;
                oidcOptions.ClientId = cookieConfiguration.ClientId;
                oidcOptions.ClientSecret = cookieConfiguration.ClientSecret;
                oidcOptions.ResponseType = cookieConfiguration.ReturnType;
                oidcOptions.GetClaimsFromUserInfoEndpoint = cookieConfiguration.GetClaimsFromUserInfoEndpoint;
                oidcOptions.SaveTokens = cookieConfiguration.SaveTokens;
                foreach (var claim in cookieConfiguration.Claims)
                {
                    oidcOptions.Scope.Add(claim);
                }
                foreach (var claim in cookieConfiguration.ClaimsMapping)
                {
                    oidcOptions.ClaimActions.MapUniqueJsonKey(claim.ClaimType, claim.JsonKey);
                }
                if (cookieConfiguration.IsRoleBasedAuthorizationActive)
                {
                    oidcOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        RoleClaimType = cookieConfiguration.RoleClaimType
                    };
                }
            });
        }
    }
}
