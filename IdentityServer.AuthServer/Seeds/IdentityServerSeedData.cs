using IdentityServer.AuthServer.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace IdentityServer.AuthServer.Seeds
{
    public class IdentityServerSeedData
    {
        public static void Seeds(ConfigurationDbContext context)
        {
            var data = Generator.GetClients();
            if (!context.Clients.Any())
            {
                foreach (var client in data.Clients)
                {
                    context.Clients.Add(client);
                }
            }
            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in data.ApiResources)
                {
                    context.ApiResources.Add(apiResource);
                }
            }
            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in data.ApiScopes)
                {
                    context.ApiScopes.Add(apiScope);
                }
            }
            if (!context.IdentityResources.Any())
            {
                data.IdentityResources.ToList().ForEach(identityResource =>
                {
                    context.IdentityResources.Add(identityResource);
                });
            }
            context.SaveChanges();
        }
    }
}
