using IdentityServer.API.Application.Dto.Client;
using IdentityServer.API.Domain;
using IdentityServer.API.Infrastructure.Exceptions;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Application.Repositories
{
    public class ApiResourceRepository : IApiResourceRepository
    {
        private readonly ConfigurationDbContext _ctx;

        public ApiResourceRepository(ConfigurationDbContext ctx, IConfiguration config)
        {
            _ctx = ctx;
        }

        public async Task<ApiResource> AddApiResourceAsync(string apiResourceName)
        {
            var apiResource = new ApiResource { Name = apiResourceName, DisplayName = apiResourceName };
            _ctx.ApiResources.Add(apiResource);
            await _ctx.SaveChangesAsync();
            return apiResource;
        }

        public async Task AddApiScopesAsync(List<ApiResourceDto> apiResources)
        {
            var allApiResources = _ctx.ApiResources.Include(c => c.Scopes).Where(x => x.Scopes != null).ToList();
            var allApiScopes = allApiResources.SelectMany(x => x.Scopes).Select(x => x.Name).Distinct().ToList();
            var listAddedScopes = new List<string>();
            foreach (var dto in apiResources)
            {
                var apiResource = _ctx.ApiResources.Include(c => c.Scopes).FirstOrDefault(x => x.Name == dto.Name) ?? await AddApiResourceAsync(dto.Name);

                if (apiResource.Scopes == null)
                    apiResource.Scopes = new List<ApiScope>();

                allApiScopes.AddRange(listAddedScopes);
                var notExistApiScopes = dto.Scopes.Except(allApiScopes).ToList();

                notExistApiScopes.ForEach(scope =>
                {
                    apiResource.Scopes.Add(new ApiScope { Name = scope, DisplayName = scope });
                });

                listAddedScopes.AddRange(notExistApiScopes);
            }

            await _ctx.SaveChangesAsync();

        }

        public async Task AddAsync(List<string> apiResources)
        {
            var existApiResourcesName = _ctx.ApiResources.Select(x => x.Name).ToList();
            var notExistResources = apiResources.Except(existApiResourcesName).Distinct().ToList();
            _ctx.ApiResources.AddRange(notExistResources.Select(x => new ApiResource
            {
                Name = x,
                DisplayName = x,
                Scopes = new List<ApiScope> { new ApiScope { Name = x } }
            }).ToList());

            await _ctx.SaveChangesAsync();
        }

        public async Task AddAsync(List<ApiResource> apiResources)
        {
            var notExistResources = apiResources.Except(_ctx.ApiResources).Distinct().ToList();
            _ctx.ApiResources.AddRange(notExistResources);
            await _ctx.SaveChangesAsync();
        }

        public ApiResource GetApiResourceByName(string apiResourceName)
        {
            return _ctx.ApiResources.Include(x => x.Scopes).FirstOrDefault(x => x.Name == apiResourceName);
        }

        public async Task UpdateDeletedApiScope(string apiResourceName, List<string> deletedScopes)
        {
            if (deletedScopes == null || deletedScopes.Count == 0) return;
            // find api resource
            var apiResource = _ctx.ApiResources.Include(x => x.Scopes).FirstOrDefault(x => x.Name == apiResourceName);
            if (apiResource == null) return;
            // get not deleted scopes and set it back
            var scopes = apiResource.Scopes?.Where(x => !deletedScopes.Contains(x.Name)).ToList();
            apiResource.Scopes = scopes;

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteApiScopes(List<string> deletedScopes)
        {
            if (deletedScopes == null || deletedScopes.Count == 0) return;
            var apiResources = _ctx.ApiResources.Include(x => x.Scopes).ToList();
            
            foreach (var apiResource in apiResources)
            {
                if (deletedScopes.Intersect(apiResource.Scopes.Select(c => c.Name)).Any())
                {
                    var scopesFromDb = _ctx.ApiResources.Include(x => x.Scopes).Where(c => c.Id == apiResource.Id).FirstOrDefault();
                    if (scopesFromDb.Scopes.Count == deletedScopes.Count)
                    {
                        _ctx.ApiResources.Remove(apiResource);
                    }
                    else
                    {
                        apiResource.Scopes = scopesFromDb.Scopes.Where(c => !deletedScopes.Contains(c.Name)).ToList();
                    }
                }
            }


            await _ctx.SaveChangesAsync();
        }
    }
}
