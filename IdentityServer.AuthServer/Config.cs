using IdentityServer4.Models;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1"){Scopes={"api1.read","api1.write","api1.update"}},
                new ApiResource("resource_api2"){Scopes={"api2.read","api2.write","api2.update"}},
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
               }
            };
        }
    }
}
