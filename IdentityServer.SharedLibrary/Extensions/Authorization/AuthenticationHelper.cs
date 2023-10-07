using IdentityServer.SharedLibrary.Configuration.TokenConfigurations;
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
    }
}
