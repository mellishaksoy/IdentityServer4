using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.API.Application.Dto.Client;
using IdentityServer.API.Domain;
using IdentityServer.API.Infrastructure.Exceptions;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using Mapster;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer.API.Mappers;
using IdentityServer.API.Application.Dto.Claim;

namespace IdentityServer.API.Application.Services.ClientService
{
    public class ClientService : IClientService
    {
        private readonly List<string> grantsForMvcClient;
        private readonly IClientRepository _clientRepository;
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly List<string> defaultScopes;

        public ClientService(IClientRepository clientRepository, IApiResourceRepository apiResourceRepository)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;

            grantsForMvcClient = new List<string>();
            grantsForMvcClient.AddRange(GrantTypes.ResourceOwnerPassword);
            grantsForMvcClient.AddRange(GrantTypes.ClientCredentials);
            grantsForMvcClient.AddRange(GrantTypes.Implicit);
            defaultScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.TokenTypes.AccessToken,
                IdentityServerConstants.TokenTypes.IdentityToken
            };

        }

        private IdentityServer4.EntityFramework.Entities.Client MapUpdateClient(IdentityServer4.EntityFramework.Entities.Client clientEntity, IdentityServer4.EntityFramework.Entities.Client clientUpdateEntity)
        {
            clientEntity.ClientSecrets = clientUpdateEntity.ClientSecrets;
            clientEntity.RedirectUris = clientUpdateEntity.RedirectUris;
            clientEntity.PostLogoutRedirectUris = clientUpdateEntity.PostLogoutRedirectUris;
            clientEntity.AbsoluteRefreshTokenLifetime = clientUpdateEntity.AbsoluteRefreshTokenLifetime;
            clientEntity.AccessTokenLifetime = clientUpdateEntity.AccessTokenLifetime;
            clientEntity.AuthorizationCodeLifetime = clientUpdateEntity.AuthorizationCodeLifetime;
            clientEntity.ConsentLifetime = clientUpdateEntity.ConsentLifetime;
            clientEntity.IdentityTokenLifetime = clientUpdateEntity.IdentityTokenLifetime;
            clientEntity.SlidingRefreshTokenLifetime = clientUpdateEntity.SlidingRefreshTokenLifetime;

            clientEntity.AllowedScopes = clientUpdateEntity.AllowedScopes;

            return clientEntity;
        }

        public async Task<ClientDto> UpdateClientAsync(string clientId, ClientUpdateDto clientUpdateDto)
        {
            var clientEntity = await _clientRepository.GetByClientIdAsync(clientId);
            if (clientEntity == null)
            {
            }

            var entityModel = clientEntity.ToModel();

            entityModel = new ClientModelMapper().MapToClientModel(entityModel, clientUpdateDto);
            
            if (clientUpdateDto.ApiResources != null)
            {
                await _apiResourceRepository.AddApiScopesAsync(clientUpdateDto.ApiResources);
                // update allowedScopes
                var apiResourceScopes = clientUpdateDto.ApiResources?.SelectMany(x => x.Scopes).ToList();
                var sameScopes = entityModel.AllowedScopes?.Intersect(apiResourceScopes).ToList();
                var newScopes = apiResourceScopes.Except(entityModel.AllowedScopes ?? new List<string>()).ToList();
                
                entityModel.AllowedScopes = sameScopes.Union(defaultScopes).ToList();
                
                newScopes.ForEach(newScope => {
                    entityModel.AllowedScopes.Add(newScope);
                });

                // deleted scopes in ClientScope table if there is no scope that is used by another api resource, 
                //the scopes must be deleted from ApiScopes table.
                foreach(var updateApiResource in clientUpdateDto.ApiResources)
                {
                    var apiResourceEntity = _apiResourceRepository.GetApiResourceByName(updateApiResource.Name);
                    var deletedScopes = apiResourceEntity.Scopes.Select(x => x.Name).Where(x => !updateApiResource.Scopes.Contains(x) && !defaultScopes.Contains(x)).ToList();
                    await _apiResourceRepository.UpdateDeletedApiScope(updateApiResource.Name, deletedScopes);
                }
                

            }
            
            // find added new secrets and deleted secrets if secrets are not null
            if (clientUpdateDto.ClientSecrets != null && clientUpdateDto.ClientSecrets.Count > 0)
            {
                entityModel.ClientSecrets = clientUpdateDto.ClientSecrets.Select(x => new Secret(x.Sha256())).ToList();
            }

            // update redirectUris
            if (clientUpdateDto.RedirectUris != null && clientUpdateDto.RedirectUris.Count > 0)
                entityModel.RedirectUris = clientUpdateDto.RedirectUris;

            // update postLogoutRedirectUris
            if (clientUpdateDto.PostLogoutRedirectUris != null && clientUpdateDto.PostLogoutRedirectUris.Count > 0)
                entityModel.PostLogoutRedirectUris = clientUpdateDto.PostLogoutRedirectUris;

            var updateEntity = entityModel.ToEntity();
            //if (updateEntity == null)
            //    throw new ClientException(code: ClientErrorCodes.ClientUpdateDtoIsNotValid, message: $"client id  {clientId} is not updated.");

            clientEntity = MapUpdateClient(clientEntity, updateEntity);

            await _clientRepository.UpdateAsync(clientEntity);

            var clientDto = new ClientModelMapper().MapToClientDto(clientEntity);
            return clientDto;
        }
        

        public async Task<List<ClientDto>> AddClientsAsync(List<ClientCreateDto> clientCreateDtos)
        {
            var clientDtos = new List<ClientDto>();
            var entities = new List<IdentityServer4.EntityFramework.Entities.Client>();
            foreach (var dto in clientCreateDtos)
            {
                
                var entityModel = new ClientModelMapper().MapToClientModel(dto);

                if (string.IsNullOrEmpty(entityModel.ClientId))
                    return null;

                entityModel.AllowedGrantTypes = grantsForMvcClient;

                // client has to have same scopes in api resources. Client scope name is not unique so we add all scopes to client allowed scopes.
                // we need this because identity server has no foreign key between client and api resources. It uses scope names for relation.
                entityModel.AllowedScopes = dto.ApiResources.SelectMany(x => x.Scopes).ToList();
                entityModel.AllowedScopes = entityModel.AllowedScopes.Union(defaultScopes).ToList();
                // add api resource scopes.
                await _apiResourceRepository.AddApiScopesAsync(dto.ApiResources);

                entities.Add(entityModel.ToEntity());
            }

            await _clientRepository.AddAsync(entities);

            clientDtos = entities.Select(x => new ClientModelMapper().MapToClientDto(x)).ToList();


            return clientDtos;
        }

        public async Task<ClientDto> GetClientAsync(string clientId)
        {
            var clientEntity = await _clientRepository.GetByClientIdAsync(clientId);
            if (clientEntity == null)
            {
                return null;
            }

            var clientDto = new ClientModelMapper().MapToClientDto(clientEntity);
            return clientDto;

        }
        
        public async Task DeleteClientAsync(string clientId)
        {
            var clientEntity = await _clientRepository.GetByClientIdAsync(clientId);
            if (clientEntity == null)
            {
                throw new Exception();
            }
            // get should be deleted scopes
            var scopesUsedByAnotherClients = _clientRepository.GetScopesNotBelongsToClientId(clientId);
            var deletableScopes = clientEntity.AllowedScopes?.Select(x => x.Scope).Except(scopesUsedByAnotherClients.Select(c=>c.Scope)).ToList();

           await _apiResourceRepository.DeleteApiScopes(deletableScopes);

           await _clientRepository.DeleteAsync(clientEntity);
        }
    }
}
