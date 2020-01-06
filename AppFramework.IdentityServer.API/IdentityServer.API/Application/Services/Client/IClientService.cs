using IdentityServer.API.Application.Dto.Claim;
using IdentityServer.API.Application.Dto.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Application.Services.ClientService
{
    public interface IClientService
    {
        Task<List<ClientDto>> AddClientsAsync(List<ClientCreateDto> clientCreateDtos);
        Task<ClientDto> UpdateClientAsync(string clientId, ClientUpdateDto clientUpdateDto);
        Task DeleteClientAsync(string clientId);
        Task<ClientDto> GetClientAsync(string clientId);
    }
}
