using IdentityServer.AuthServer.Configuration.Constants;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1"){Scopes={"api1.read","api1.write","api1.update"},ApiSecrets = new[]{new Secret("secretapi1".Sha256())}},
                new ApiResource("resource_api2"){Scopes={"api2.read","api2.write","api2.update"},ApiSecrets = new[]{new Secret("secretapi2".Sha256())}},
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read","Read permission API1."),
                new ApiScope("api1.write","Write permission API1."),
                new ApiScope("api1.update","Update permission API1."),
                new ApiScope("api2.read","Read permission API2."),
                new ApiScope("api2.write","Write permission API2."),
                new ApiScope("api2.update","Update permission API2.")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
               new Client()
               {
                   ClientId = "Client1",
                   ClientName = "Client1App",
                   ClientSecrets = new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes = GrantTypes.ClientCredentials,
                   AllowedScopes = {"api1.read","api1.update","api1.write" }
               },
                new Client()
               {
                   ClientId = "Client2",
                   ClientName = "Client2App",
                   ClientSecrets = new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes = GrantTypes.ClientCredentials,
                   AllowedScopes = {"api1.read","api2.write","api2.update"}
               },
               new Client()
               {
                   ClientId = "Client3MVC",
                   ClientName = "Client3 MVC App",
                   RequirePkce = false,
                   ClientSecrets = new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes = GrantTypes.Hybrid,
                   RedirectUris = GetEndpoint.GetRedirectUris(new List<ApplicationCode>{ApplicationCode.Client3Mvc}),
                   PostLogoutRedirectUris = GetEndpoint.GetPostLogoutRedirectUris(new List<ApplicationCode>{ApplicationCode.Client3Mvc}),
                   AllowedScopes = { IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read","api2.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                   AccessTokenLifetime = 2*60*60,
                   AllowOfflineAccess = true,
                   RefreshTokenUsage = TokenUsage.OneTimeOnly,
                   RefreshTokenExpiration = TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                   RequireConsent = true,
               },
               new Client()
               {
                   ClientId = "Client2MVC",
                   ClientName = "Client2 MVC App",
                   RequirePkce = false,
                   ClientSecrets = new[] {new Secret("secret".Sha256())},
                   AllowedGrantTypes = GrantTypes.Hybrid,
                   RedirectUris = GetEndpoint.GetRedirectUris(new List<ApplicationCode>{ApplicationCode.Client2Mvc}),
                   PostLogoutRedirectUris = GetEndpoint.GetPostLogoutRedirectUris(new List<ApplicationCode>{ApplicationCode.Client2Mvc}),
                   AllowedScopes = { IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read","api2.read", IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                   AccessTokenLifetime = 2*60*60,
                   AllowOfflineAccess = true,
                   RefreshTokenUsage = TokenUsage.OneTimeOnly,
                   RefreshTokenExpiration = TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                   RequireConsent = true,
               },
               new Client()
               {
                   ClientId = "js-Client",
                   RequireClientSecret = false,
                   AllowedGrantTypes = GrantTypes.Code,
                   ClientName = "Js Client (Angular)",
                   AllowedScopes = { IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read","api2.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                   RedirectUris = GetEndpoint.GetRedirectUris(new List<ApplicationCode>{ApplicationCode.SpaAngular}),
                   AllowedCorsOrigins = {"http://localhost:4200/"},
                   PostLogoutRedirectUris =  {"http://localhost:4200/"},
               }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(){Name = "CountryAndCity",DisplayName="Country And City",Description = "User's country and city information.",UserClaims = new []{"country","city"} },
                new IdentityResource(){Name="Roles",DisplayName = "Roles",Description="User Roles",UserClaims=new []{"role"}}
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
              new TestUser() {SubjectId="1",Username="alicanyilmaz101@gmail.com",Password="Alican123",Claims=new List<Claim>(){
                  new Claim("given_name","Alican"),
                  new Claim("family_name", "Yılmaz"),
                  new Claim("country", "Turkey"),
                  new Claim("city", "Hatay"),
                  new Claim("role","admin")
              }},
               new TestUser() {SubjectId="2",Username="test@gmail.com",Password="Test123",Claims=new List<Claim>(){
                  new Claim("given_name","TestName"),
                  new Claim("family_name", "TestSurname"),
                  new Claim("country", "Turkey"),
                  new Claim("city", "Ankara"),
                  new Claim("role","customer")
              }}
            };
        }
    }
}
