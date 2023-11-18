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
               }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
              new TestUser() {SubjectId="1",Username="alicanyilmaz101@gmail.com",Password="Alican123",Claims=new List<Claim>(){
                  new Claim("given_name","Alican"),
                  new Claim("family_name", "Yılmaz")
              }},
               new TestUser() {SubjectId="1",Username="test@gmail.com",Password="Test123",Claims=new List<Claim>(){
                  new Claim("given_name","TestName"),
                  new Claim("family_name", "TestSurname")
              }}
            };
        }
    }
}
