using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.API.Data
{
    public class ApplicationConfigurationDbContext : ConfigurationDbContext<ConfigurationDbContext>
    {
        public ApplicationConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
            
        }
    }
} 