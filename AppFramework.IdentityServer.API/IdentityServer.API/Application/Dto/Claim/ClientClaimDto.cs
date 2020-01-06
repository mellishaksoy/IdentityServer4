using System;
using System.Collections.Generic;

namespace IdentityServer.API.Application.Dto.Claim
{
    [Serializable]
    public class ClientClaimDto
    {
        public string ClientId { get; set; }
        public List<Claim> Claims { get; set; }
    }

    [Serializable]
    public class ClientClaimCreateDto 
    {
        public ClientClaimCreateDto()
        {
        }
        public string ClientId { get; set; }
        public List<Claim> Claims { get; set; }
    }
    
    public class Claim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
