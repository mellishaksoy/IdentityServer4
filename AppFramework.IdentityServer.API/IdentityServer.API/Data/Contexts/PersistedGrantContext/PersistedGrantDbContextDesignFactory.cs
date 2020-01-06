using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.API.Data.Contexts.PersistedGrantContext
{
    public class PersistedGrantDbContextDesignFactory : IDesignTimeDbContextFactory<ApplicationPersistedGrantDbContext>
    {
        public ApplicationPersistedGrantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationPersistedGrantDbContext>()
                .UseSqlServer(
                    "Data Source = 127.0.0.1,1453; Initial Catalog = IdentityServerDb; User ID=sa;Password='Parola_2018*'; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            var storeOptions = new OperationalStoreOptions();
            return new ApplicationPersistedGrantDbContext(optionsBuilder.Options, storeOptions);
        }

       
    }
}
