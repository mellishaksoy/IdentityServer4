
using IdentityServer.API.Application.Dto.Client;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Domain
{
    public interface IApiResourceRepository
    {
        Task AddAsync(List<string> apiResources);
        Task AddAsync(List<ApiResource> apiResources);
        Task AddApiScopesAsync(List<ApiResourceDto> apiResources);
        Task<ApiResource> AddApiResourceAsync(string apiResourceName);
        Task DeleteApiScopes(List<string> deletedScopes);
        ApiResource GetApiResourceByName(string apiResourceName);
        Task UpdateDeletedApiScope(string apiResourceName, List<string> deletedScopes);
    }
}
