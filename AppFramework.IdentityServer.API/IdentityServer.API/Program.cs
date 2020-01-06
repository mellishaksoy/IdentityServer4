using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer.API.Data;
using IdentityServer.API.Settings;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Winton.Extensions.Configuration.Consul;

namespace IdentityServer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var consulAddress = Environment.GetEnvironmentVariable("CONSUL_ADDRESS");
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                 .MinimumLevel.Override("System", LogEventLevel.Warning)
                 .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                 .Enrich.FromLogContext()
                 .WriteTo.File("C:\\idsrv4.txt", rollingInterval: RollingInterval.Day).CreateLogger();//(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate) */
 
                 /*.CreateLogger();*/
            
           

            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostingContext, builder) =>
                    {
                        builder
                            .AddConsul(
                                $"identity.api/{environment}",
                                cancellationTokenSource.Token,
                                options =>
                                {
                                    options.ConsulConfigurationOptions =
                                        cco => { cco.Address = new Uri(consulAddress); };
                                    options.Optional = false;
                                    options.ReloadOnChange = true;
                                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                                })
                            .AddEnvironmentVariables();
                    })
                .UseStartup<Startup>().UseSerilog().Build()
                    .Run();
        }
    }
}
