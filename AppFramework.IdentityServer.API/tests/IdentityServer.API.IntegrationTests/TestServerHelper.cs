using Microsoft.AspNetCore.TestHost;
using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using IdentityServer.API.Data;


namespace IdentityServer.API.IntegrationTests
{
    public static class TestServerHelper
    {
        private static readonly HttpClient Client;
        private static readonly Guid TenantId;
        private static readonly string BaseUrl;
        private static readonly ApplicationDbContext Context;

#pragma warning disable S3963 // "static" fields should be initialized inline
        static TestServerHelper()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

            var projectDir = System.IO.Directory.GetCurrentDirectory();
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            Client = server.CreateClient();
            TenantId = Guid.NewGuid();
            Client.DefaultRequestHeaders.Add("tenantId", TenantId.ToString());
            BaseUrl = "http://localhost/api/v1";
            Context = server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            new ApplicationDbContextSeed().SeedAsyncTest(Context).Wait();
        }
#pragma warning restore S3963 // "static" fields should be initialized inline

        public static string CreateUsersRequestUrl(params object[] args)
        {
            var newUrl = BaseUrl + "/users";
            return args.Aggregate(newUrl, (current, p) => current + "/" + p);
        }

        public static Guid GetTenantId()
        {
            return TenantId;
        }

        public static ApplicationDbContext GetContext()
        {
            return Context;
        }

        public static HttpClient GetClient()
        {
            return Client;
        }
    }
}
