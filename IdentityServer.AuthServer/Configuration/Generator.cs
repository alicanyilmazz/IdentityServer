using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IdentityServer.AuthServer.Configuration
{
    public class Generator
    {
        private static IConfiguration _configuration;
        public static IdentityServerData GetClients()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
            IdentityServerData identityServerData = new IdentityServerData();
            var clientsSection = _configuration.GetSection("Clients");
            identityServerData.Clients = clientsSection.Get<List<IdentityServer4.EntityFramework.Entities.Client>>();
            var apiResorcesSection = _configuration.GetSection("ApiResorce");
            identityServerData.ApiResources = apiResorcesSection.Get<List<IdentityServer4.EntityFramework.Entities.ApiResource>>();
            var identityResorcesSection = _configuration.GetSection("IdentityResources");
            identityServerData.IdentityResources = identityResorcesSection.Get<List<IdentityServer4.EntityFramework.Entities.IdentityResource>>();
            var apiScopeSection = _configuration.GetSection("ApiScopes");
            identityServerData.ApiScopes = apiScopeSection.Get<List<IdentityServer4.EntityFramework.Entities.ApiScope>>();
            return identityServerData;
        }
    }
    public class IdentityServerData
    {
        public List<IdentityServer4.EntityFramework.Entities.Client> Clients { get; set; } = new();
        public List<IdentityServer4.EntityFramework.Entities.ApiResource> ApiResources { get; set; } = new();
        public List<IdentityServer4.EntityFramework.Entities.IdentityResource> IdentityResources { get; set; } = new();
        public List<IdentityServer4.EntityFramework.Entities.ApiScope> ApiScopes { get; set; } = new();
    }
}
