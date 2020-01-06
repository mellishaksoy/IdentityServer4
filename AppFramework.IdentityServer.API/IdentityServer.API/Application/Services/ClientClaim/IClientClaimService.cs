using IdentityServer.API.Application.Dto.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Application.Services.ClientClaim
{
    public interface IClientClaimService
    {
        Task<ClientClaimDto> AddClientClaims(ClientClaimCreateDto clientClaimCreateDto);
        Task<ClientClaimDto> GetClientClaimsAsync(string clientId);
    }
}
