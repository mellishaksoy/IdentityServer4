using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityServer.API.Application.Dto.Client;
using IdentityServer.API.Application.Services.ClientService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Quickstart.RestServices
{
    [Route("api/v1/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateClients([FromBody]List<ClientCreateDto> dtos)
        {
            var result = await _clientService.AddClientsAsync(dtos);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateClient([FromHeader] string clientId, [FromBody]ClientUpdateDto dto)
        {
            var result = await _clientService.UpdateClientAsync(clientId, dto);
            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteClient([FromHeader] string clientId)
        {
            await _clientService.DeleteClientAsync(clientId);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ClientDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetClient([FromHeader] string clientId)
        {
            var result = await _clientService.GetClientAsync(clientId);
            return Ok(result);
        }
    }
}