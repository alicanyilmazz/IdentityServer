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
    public static class AuthorizationManager
    {
        public static void AddCustomAuthentication(this WebApplicationBuilder builder , string authority , string audience)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
            {
                option.Authority = authority;
                option.Audience = audience;
            });
        }
    }
}
