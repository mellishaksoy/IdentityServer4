using IdentityServer.API.Application.Dto.Tenant;
using IdentityServer.API.Application.Repositories;
using IdentityServer.API.Application.Services.ClientClaim;
using IdentityServer.API.Application.Services.ClientService;
using IdentityServer.API.Application.Services.JobTitle;
using IdentityServer.API.Application.Services.User;
using IdentityServer.API.Data;
using IdentityServer.API.Domain;
using IdentityServer.API.Infrastructure.Filters;
using IdentityServer.API.Infrastructure.ProfileExtensions;
using IdentityServer.API.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer.API
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddEFConfigurationStore(
            this IIdentityServerBuilder builder, string connectionString)
        {
            string assemblyNamespace = typeof(IdentityServerBuilderExtensions)
                .GetTypeInfo()
                .Assembly
                .GetName()
                .Name;
            builder.AddConfigurationStore(options =>
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, optionsBuilder =>
                        optionsBuilder.MigrationsAssembly(assemblyNamespace)
                    )
            );
            return builder;
        }
    }
    public class Startup
    {
        private readonly string IdentityCors = "_identitySpecificOrigins";

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            var connectionString = Configuration["ConnectionString"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services.AddCors(options =>
            {
                options.AddPolicy(IdentityCors,
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader()
                                    .AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("Location");
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(IdentityCors,
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader()
                                    .AllowAnyMethod().AllowAnyOrigin();
                });
            });

       

            Console.WriteLine(connectionString);


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableSensitiveDataLogging(true);
                options.UseSqlServer(Configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(migrationsAssembly);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
            services.AddMvc(o =>
            {
               
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();
            services.AddDbContext<ConfigurationDbContext>(builder =>
              builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly).MigrationsHistoryTable("configStoreHistory")));
            services.AddDbContext<ApplicationDbContext>(builder =>
                builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly).MigrationsHistoryTable("operationalStoreHistory")));
            services.AddDbContext<PersistedGrantDbContext>(builder =>
               builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly).MigrationsHistoryTable("grantsStoreHistory")));
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
            var t = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            }).AddTestUsers(Config.GetUsers())
               .AddConfigurationStore(options =>
                     options.ConfigureDbContext = builder =>
                         builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly).MigrationsHistoryTable("configStoreHistory")
                             ))
               .AddOperationalStore(options =>
                     options.ConfigureDbContext = builder =>
                         builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly).MigrationsHistoryTable("operationalStoreHistory")))
               .AddProfileService<CustomProfileService>()
               .AddAspNetIdentity<ApplicationUser>()
               .AddInMemoryApiResources(Config.GetApiResources())
               .AddInMemoryClients(Config.GetClients())
               .AddInMemoryClients(Config.GetTestClients())
               .AddInMemoryIdentityResources(Config.GetIdentityResources())
               .AddDeveloperSigningCredential();
               //.AddSigningCredential(LoadCertificate());
               
            AddEFConfigurationStore(t, connectionString);

            services.AddTransient<IClientClaimService, ClientClaimService>();
            services.AddTransient<IJobTitleService, JobTitleService>();
            services.AddTransient<IJobtitleRepository, JobtitleRepository>();

            
            services.AddTransient<IProfileService, CustomProfileService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IApiResourceRepository, ApiResourceRepository>();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor() { HttpContext = new DefaultHttpContext() { RequestServices = services.BuildServiceProvider() } });
            services.AddAuthentication();
        }

        public X509Certificate2 LoadCertificate()
        {
            string thumbPrint = Configuration["CertificateThumbprint"];
            // Starting with the .NET Framework 4.6, X509Store implements IDisposable.
            // On older .NET, store.Close should be called.
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, validOnly: false);
                if (certCollection.Count == 0)
                    throw new Exception("No certificate found containing the specified thumbprint.");

                return certCollection[0];
            }
        }

        public static IIdentityServerBuilder AddEFConfigurationStore(
               IIdentityServerBuilder builder, string connectionString)
        {
            string assemblyNamespace = typeof(IdentityServerBuilderExtensions)
                .GetTypeInfo()
                .Assembly
                .GetName()
                .Name;
            builder.AddConfigurationStore(options =>
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, optionsBuilder =>
                        optionsBuilder.MigrationsAssembly(assemblyNamespace)
                    )
            );
            return builder;
        }
        private static void InitializeDbTestData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.EnsureCreated();
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
   
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if (!userManager.Users.Any())
                {
                    foreach (var testUser in Config.GetUsers())
                    {
                        var identityUser = new ApplicationUser(testUser.Username)
                        {
                            Id = Guid.NewGuid()
                        };
                        userManager.CreateAsync(identityUser, testUser.Password).Wait();
                        userManager.AddClaimsAsync(identityUser, testUser.Claims.ToList()).Wait();
                    }
                }
            }
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(IdentityCors);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeDbTestData(app);
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}