using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.API.Data.Contexts.ConfigurationContext
{
    public class ApplicationConfigurationDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationConfigurationDbContext>
    {
        public ApplicationConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>()
                .UseSqlServer(
                    "Data Source = 127.0.0.1,1453; Initial Catalog = IdentityServerDb; User ID=sa;Password='Parola_2018*'; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            var storeOptions = new ConfigurationStoreOptions();
            return new ApplicationConfigurationDbContext(optionsBuilder.Options, storeOptions);
        }
    }
}