using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace IdentityServer.API.Data
{
    public class ApplicationDbContextSeed /*: IContextSeed*/
    {
        //public Task SeedAsync<TOptions, TContextSeed>(DbContext context, IHostingEnvironment env, IOptions<TOptions> apiSettings,
        //    ILogger<TContextSeed> logger) where TOptions : class, new() where TContextSeed : IContextSeed, new()
        //{
        //    return Task.CompletedTask;
        //}

        //public Task SeedAsyncTest(DbContext context)
        //{
        //    return Task.CompletedTask;
        //}

        //public Policy CreatePolicy<TSeed>(ILogger<TSeed> logger, string prefix, int retries = 3) where TSeed : IContextSeed, new()
        //{
        //    return null;
        //}
    }
}
