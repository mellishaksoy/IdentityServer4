using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.API.Data
{
    public class ApplicationPersistedGrantDbContext : PersistedGrantDbContext<PersistedGrantDbContext>
    {
        public ApplicationPersistedGrantDbContext(DbContextOptions<ApplicationPersistedGrantDbContext> options, OperationalStoreOptions storeOptions)
            : base(options, storeOptions)
        {
        }
    }
}
