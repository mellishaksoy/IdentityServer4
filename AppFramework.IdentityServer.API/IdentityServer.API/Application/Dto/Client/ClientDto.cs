using System;
using System.Collections.Generic;

namespace IdentityServer.API.Application.Dto.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public List<string> ClientSecrets { get; set; }
        public List<string> AllowedScopes { get; set; }

        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int ConsentLifetime { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        
    }

    [Serializable]
    public class ClientCreateDto 
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        
        public List<string> ClientSecrets { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }

        public int? AbsoluteRefreshTokenLifetime { get; set; }
        public int? AccessTokenLifetime { get; set; }
        public int? AuthorizationCodeLifetime { get; set; }
        public int? ConsentLifetime { get; set; }
        public int? IdentityTokenLifetime { get; set; }
        public int? SlidingRefreshTokenLifetime { get; set; }

        public List<ApiResourceDto> ApiResources { get; set; }
    }

    [Serializable]
    public class ClientUpdateDto 
    {
        public ClientUpdateDto()
        {
            ClientSecrets = new List<string>();
            RedirectUris = new List<string>();
            PostLogoutRedirectUris = new List<string>();
            ApiResources = new List<ApiResourceDto>();
        }

        public List<string> ClientSecrets { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }

        public int? AbsoluteRefreshTokenLifetime { get; set; }
        public int? AccessTokenLifetime { get; set; }
        public int? AuthorizationCodeLifetime { get; set; }
        public int? ConsentLifetime { get; set; }
        public int? IdentityTokenLifetime { get; set; }
        public int? SlidingRefreshTokenLifetime { get; set; }

        public List<ApiResourceDto> ApiResources { get; set; }
    }

    public class ApiResourceDto
    {
        public string Name { get; set; }
        public List<string> Scopes { get; set; }
    }

}
