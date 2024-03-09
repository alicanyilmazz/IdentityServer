using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;

namespace IdentityServer.AuthServer.Seeds
{
    public class ConfigurationDbContextInitializer
    {
        public List<IdentityServer4.EntityFramework.Entities.Client> Client { get; set; } = new();
        public List<IdentityServer4.EntityFramework.Entities.ApiResource> ApiResources { get; set; } = new();
        public List<IdentityServer4.EntityFramework.Entities.ApiScope> ApiScopes { get; set; } = new();
        public List<IdentityServer4.EntityFramework.Entities.IdentityResource> IdentityResources { get; set; } = new();
        public List<IdentityServer4.EntityFramework.Entities.Client> GetClient()
        {
            Client.Add(new IdentityServer4.EntityFramework.Entities.Client
            {
                ClientId = "Client1",
                ClientName = "Client1App",
                ClientSecrets = new List<ClientSecret> { new ClientSecret { Value = "secret".Sha256() } },
                AllowedGrantTypes = new List<ClientGrantType> { new ClientGrantType { GrantType = GrantTypes.ClientCredentials.ToString() } },
                AllowedScopes = new List<ClientScope> { new ClientScope { Scope = "api1.read" }, new ClientScope { Scope = "api1.update" }, new ClientScope { Scope = "api1.write" } }
            });
            Client.Add(new IdentityServer4.EntityFramework.Entities.Client
            {
                ClientId = "Client2",
                ClientName = "Client2App",
                ClientSecrets = new List<ClientSecret> { new ClientSecret { Value = "secret".Sha256() } },
                AllowedGrantTypes = new List<ClientGrantType> { new ClientGrantType { GrantType = GrantTypes.ClientCredentials.ToString() } },
                AllowedScopes = new List<ClientScope> { new ClientScope { Scope = "api1.read" }, new ClientScope { Scope = "api2.write" }, new ClientScope { Scope = "api2.update" } }
            });
            Client.Add(new IdentityServer4.EntityFramework.Entities.Client
            {
                ClientId = "Client3MVC",
                ClientName = "Client3MVC",
                RequirePkce = false,
                ClientSecrets = new List<ClientSecret> { new ClientSecret { Value = "secret".Sha256() } },
                RedirectUris = new List<ClientRedirectUri> { new ClientRedirectUri { RedirectUri = "https://localhost:7290/signin-oidc" } },
                PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri> { new ClientPostLogoutRedirectUri { PostLogoutRedirectUri = "https://localhost:7290/signout-callback-oidc" } },
                AllowedGrantTypes = new List<ClientGrantType> { new ClientGrantType { GrantType = GrantTypes.Hybrid.ToString() } },
                AllowedScopes = new List<ClientScope> { new ClientScope { Scope = "api1.read" }, new ClientScope { Scope = "api2.read" }, new ClientScope { Scope = "openid" }, new ClientScope { Scope = "profile" }, new ClientScope { Scope = "offline_access" }, new ClientScope { Scope = "CountryAndCity" }, new ClientScope { Scope = "Roles" } },
                AccessTokenLifetime = 2 * 60 * 60,
                AllowOfflineAccess = true,
                RefreshTokenUsage = (int)TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = (int)TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                RequireConsent = true,
            });
            Client.Add(new IdentityServer4.EntityFramework.Entities.Client
            {
                ClientId = "Client2MVC",
                ClientName = "Client2MVC",
                RequirePkce = false,
                ClientSecrets = new List<ClientSecret> { new ClientSecret { Value = "secret".Sha256() } },
                RedirectUris = new List<ClientRedirectUri> { new ClientRedirectUri { RedirectUri = "https://localhost:7290/signin-oidc" } },
                PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri> { new ClientPostLogoutRedirectUri { PostLogoutRedirectUri = "https://localhost:7290/signout-callback-oidc" } },
                AllowedGrantTypes = new List<ClientGrantType> { new ClientGrantType { GrantType = GrantTypes.Hybrid.ToString() } },
                AllowedScopes = new List<ClientScope> { new ClientScope { Scope = "api1.read" }, new ClientScope { Scope = "api2.read" }, new ClientScope { Scope = "openid" }, new ClientScope { Scope = "profile" }, new ClientScope { Scope = "offline_access" }, new ClientScope { Scope = "CountryAndCity" }, new ClientScope { Scope = "Roles" } },
                AccessTokenLifetime = 2 * 60 * 60,
                AllowOfflineAccess = true,
                RefreshTokenUsage = (int)TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = (int)TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                RequireConsent = true,
            });
            return Client;
        }
        public List<IdentityServer4.EntityFramework.Entities.ApiResource> GetApiResources()
        {
            ApiResources.Add(new IdentityServer4.EntityFramework.Entities.ApiResource
            {
                Name = "resource_api1",
                Scopes = new List<ApiResourceScope> { new ApiResourceScope { Scope = "api1.read" }, new ApiResourceScope { Scope = "api1.write" }, new ApiResourceScope { Scope = "api1.update" } },
                Secrets = new List<ApiResourceSecret> { new ApiResourceSecret { Value = "secretapi1".Sha256() } }
            });
            ApiResources.Add(new IdentityServer4.EntityFramework.Entities.ApiResource
            {
                Name = "resource_api2",
                Scopes = new List<ApiResourceScope> { new ApiResourceScope { Scope = "api2.read" }, new ApiResourceScope { Scope = "api2.write" }, new ApiResourceScope { Scope = "api2.update" } },
                Secrets = new List<ApiResourceSecret> { new ApiResourceSecret { Value = "secretapi2".Sha256() } }
            });
            return ApiResources;
        }

        public List<IdentityServer4.EntityFramework.Entities.ApiScope> GetApiScopes()
        {
            ApiScopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope
            {
                Name = "api1.read",
                DisplayName = "Read permission API1."
            });
            ApiScopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope
            {
                Name = "api1.write",
                DisplayName = "Write permission API1."
            });
            ApiScopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope
            {
                Name = "api1.update",
                DisplayName = "Update permission API1."
            });
            ApiScopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope
            {
                Name = "api2.read",
                DisplayName = "Read permission API2."
            });
            ApiScopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope
            {
                Name = "api2.write",
                DisplayName = "Write permission API2."
            });
            ApiScopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope
            {
                Name = "api2.update",
                DisplayName = "Update permission API2."
            });
            return ApiScopes;
        }
        public List<IdentityServer4.EntityFramework.Entities.IdentityResource> GettIdentityResources()
        {
            IdentityResources.Add(new IdentityServer4.EntityFramework.Entities.IdentityResource
            {
                Name = "openid",
                DisplayName = "Your user identifier",
                Required = true,
                UserClaims = new List<IdentityResourceClaim>
                 {
                     new IdentityResourceClaim
                     {
                        Type = "sub"
                     },
                 }
            });
            IdentityResources.Add(new IdentityServer4.EntityFramework.Entities.IdentityResource
            {
                Name = "profile",
                DisplayName = "User profile",
                Description = "Your user profile information (first name, last name, etc.)",
                Required = true,
                Emphasize = true,
                UserClaims = new List<IdentityResourceClaim>
                 {
                     new IdentityResourceClaim
                     {
                         Type = "name"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "family_name"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "given_name"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "middle_name"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "profile"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "email"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "website"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "picture"
                     },
                     new IdentityResourceClaim
                     {
                         Type = "gender"
                     },
                 }
            });
            IdentityResources.Add(new IdentityServer4.EntityFramework.Entities.IdentityResource
            {
                Name = "CountryAndCity",
                DisplayName = "Country And City",
                Required = false,
                Description = "User's country and city information.",
                UserClaims = new List<IdentityResourceClaim>
                 {
                     new IdentityResourceClaim
                     {
                         Type = "country"
                     },
                      new IdentityResourceClaim
                     {
                        Type = "city"
                     },
                 }
            });
            IdentityResources.Add(new IdentityServer4.EntityFramework.Entities.IdentityResource
            {
                Name = "Roles",
                DisplayName = "Roles",
                Required = false,
                Description = "User's Roles.",
                UserClaims = new List<IdentityResourceClaim>
                 {
                     new IdentityResourceClaim
                     {
                        Type = "role"
                     }
                 }
            });
            return IdentityResources;
        }
    }
}
