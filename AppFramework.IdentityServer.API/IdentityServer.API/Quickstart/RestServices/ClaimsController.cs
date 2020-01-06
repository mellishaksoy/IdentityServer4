using IdentityServer.API.Application.Dto.Claim;
using IdentityServer.API.Application.Services.ClientClaim;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace IdentityServer.API.Quickstart.RestServices
{
    [Route("api/v1/[controller]")]
    public class ClaimsController : Controller
    {
        private readonly IClientClaimService _clientClaimService;

        public ClaimsController(IClientClaimService clientClaimService)
        {
            _clientClaimService = clientClaimService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateClientClaims([FromBody]ClientClaimCreateDto clientClaimCreateDto)
        {
            var result = await _clientClaimService.AddClientClaims(clientClaimCreateDto);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ClientClaimDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetClient([FromHeader] string clientId)
        {
            var result = await _clientClaimService.GetClientClaimsAsync(clientId);
            return Ok(result);
        }
    }
}