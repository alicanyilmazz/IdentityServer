using IdentityServer4.EntityFramework.DbContexts;

namespace IdentityServer.AuthServer.Seeds
{
    public class IdentityServerSeedData
    {
        public static void Seeds(ConfigurationDbContext context)
        {
            ConfigurationDbContextInitializer configurationManager = new ConfigurationDbContextInitializer();
            if (!context.Clients.Any())
            {
                foreach (var client in configurationManager.GetClient())
                {
                    context.Clients.Add(client);
                }
            }
            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in configurationManager.GetApiResources())
                {
                    context.ApiResources.Add(apiResource);
                }
            }
            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in configurationManager.GetApiScopes())
                {
                    context.ApiScopes.Add(apiScope);
                }
            }
            if (!context.IdentityResources.Any())
            {
                configurationManager.GettIdentityResources().ToList().ForEach(identityResource =>
                {
                    context.IdentityResources.Add(identityResource);
                });
            }
            context.SaveChanges();
        }
    }
}
