using IdentityServer.API.Application.Dto.Claim;
using IdentityServer.API.Domain;
using IdentityServer.API.Infrastructure.Exceptions;
using IdentityServer4.EntityFramework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Application.Services.ClientClaim
{
    public class ClientClaimService : IClientClaimService
    {
        private readonly IClientRepository _clientRepository;

        public ClientClaimService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientClaimDto> AddClientClaims(ClientClaimCreateDto clientClaimCreateDto)
        {
            var clientId = clientClaimCreateDto.ClientId;
            var clientEntity = await _clientRepository.GetByClientIdAsync(clientId);
            if (clientEntity == null)
            {
                return null;
            }
            
            var entityModel = clientEntity.ToModel();
            foreach (var claim in clientClaimCreateDto.Claims)
                entityModel.Claims.Add(new System.Security.Claims.Claim(claim.Type, claim.Value));

            var updateEntity = entityModel.ToEntity();

            clientEntity.Claims = updateEntity.Claims;

            await _clientRepository.UpdateAsync(clientEntity);

            return new ClientClaimDto
            {
                ClientId = clientClaimCreateDto.ClientId,
                Claims = clientClaimCreateDto.Claims
            };
        }

        public async Task<ClientClaimDto> GetClientClaimsAsync(string clientId)
        {
            var clientEntity = await _clientRepository.GetByClientIdAsync(clientId);
            if (clientEntity == null)
            {
                return null;
            }

            var entityModel = clientEntity.ToModel();

            return new ClientClaimDto
            {
                ClientId = clientId,
                Claims = entityModel.Claims.Select(x => new Claim { Type = x.Type, Value=x.Value}).ToList()
            };
        }
    }
}
