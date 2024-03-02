using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.SharedLibrary.Configuration.CookieConfigurations
{
    public class CookieConfiguration
    {
        public string? DefaultScheme { get; set; }
        public string? DefaultChallengeScheme { get; set; }
        public string? Authority { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? ReturnType { get; set; }
        public bool GetClaimsFromUserInfoEndpoint { get; set; }
        public bool SaveTokens { get; set; }
        public string? AccessDeniedPath { get; set; }
        public List<string> Claims { get; set; } = new List<string>();
        public List<ClaimsMappingProperties> ClaimsMapping { get; set; } = new List<ClaimsMappingProperties>();
        public bool IsRoleBasedAuthorizationActive { get; set; }
        public string? RoleClaimType { get; set; }
    }
    public class ClaimsMappingProperties
    {
        public string? ClaimType { get; set; }
        public string? JsonKey { get; set; }
    }

    public class BasicCookieConfiguration
    {
        public string? DefaultScheme { get; set; }
        public string? AccessDeniedPath { get; set; }
        /// <summary>
        /// If the user is not a member, it is used to determine the page that only members can see.
        /// </summary>
        public string? LoginPath { get; set; }
        public string? LogoutPath { get; set; }
    }

}
