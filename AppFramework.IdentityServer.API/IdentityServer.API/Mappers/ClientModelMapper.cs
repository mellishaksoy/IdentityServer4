using IdentityServer.API.Application.Dto.Client;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Mappers
{
    public class ClientModelMapper
    {

        public Client MapToClientModel(Client client,ClientUpdateDto dto)
        {
            if (dto == null) return new Client();
            if(client == null) client = new Client();
        
            client.ClientSecrets = dto.ClientSecrets?.Select(x => new Secret(x.Sha256())).ToList();
            client.AbsoluteRefreshTokenLifetime = dto.AbsoluteRefreshTokenLifetime == null ? Int32.MaxValue : (int)dto.AbsoluteRefreshTokenLifetime;
            client.AccessTokenLifetime = dto.AccessTokenLifetime == null ? Int32.MaxValue : (int)dto.AccessTokenLifetime;
            client.AuthorizationCodeLifetime = dto.AuthorizationCodeLifetime == null ? Int32.MaxValue : (int)dto.AuthorizationCodeLifetime;
            client.ConsentLifetime = dto.ConsentLifetime == null ? Int32.MaxValue : (int)dto.ConsentLifetime;
            client.IdentityTokenLifetime = dto.IdentityTokenLifetime == null ? Int32.MaxValue : (int)dto.IdentityTokenLifetime;
            client.SlidingRefreshTokenLifetime = dto.SlidingRefreshTokenLifetime == null ? Int32.MaxValue : (int)dto.SlidingRefreshTokenLifetime;

            return client;
        }

        public Client MapToClientModel(ClientCreateDto dto)
        {
            var client = new Client
            {
                ClientName = string.IsNullOrEmpty(dto.ClientName) ? dto.ClientId : dto.ClientName,
                ClientId = dto.ClientId,
                ClientSecrets = dto.ClientSecrets?.Select(x => new Secret(x.Sha256())).ToList(),
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                AllowAccessTokensViaBrowser = true,
                AbsoluteRefreshTokenLifetime = dto.AbsoluteRefreshTokenLifetime == null ? Int32.MaxValue : (int)dto.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = dto.AccessTokenLifetime == null ? Int32.MaxValue : (int)dto.AccessTokenLifetime,
                AuthorizationCodeLifetime = dto.AuthorizationCodeLifetime == null ? Int32.MaxValue : (int)dto.AuthorizationCodeLifetime,
                ConsentLifetime = dto.ConsentLifetime == null ? Int32.MaxValue : (int)dto.ConsentLifetime,
                IdentityTokenLifetime = dto.IdentityTokenLifetime == null ? Int32.MaxValue : (int)dto.IdentityTokenLifetime,
                SlidingRefreshTokenLifetime = dto.SlidingRefreshTokenLifetime == null ? Int32.MaxValue : (int)dto.SlidingRefreshTokenLifetime,
                RedirectUris = dto.RedirectUris,
                PostLogoutRedirectUris = dto.PostLogoutRedirectUris
            };
            
            #region default AllowedScopes
            client.AllowedScopes.Add(IdentityServerConstants.StandardScopes.OpenId);
            client.AllowedScopes.Add(IdentityServerConstants.StandardScopes.Profile);
            client.AllowedScopes.Add(IdentityServerConstants.TokenTypes.AccessToken);
            client.AllowedScopes.Add(IdentityServerConstants.TokenTypes.IdentityToken);
            #endregion
            
            return client;
        }

        public ClientDto MapToClientDto(IdentityServer4.EntityFramework.Entities.Client entity)
        {
            var clientDto = new ClientDto();
            clientDto.Id = entity.Id;
            clientDto.ClientId = entity.ClientId;
            clientDto.ClientName = entity.ClientName;
            clientDto.ClientSecrets = entity.ClientSecrets?.Select(x => x.Value).ToList();
            clientDto.AllowedScopes = entity.AllowedScopes?.Select(x => x.Scope).ToList();

            clientDto.RedirectUris = entity.RedirectUris?.Select(x => x.RedirectUri).ToList();
            clientDto.PostLogoutRedirectUris = entity.PostLogoutRedirectUris?.Select(x => x.PostLogoutRedirectUri).ToList();
            
            clientDto.AbsoluteRefreshTokenLifetime = entity.AbsoluteRefreshTokenLifetime;
            clientDto.AccessTokenLifetime = entity.AccessTokenLifetime;
            clientDto.AuthorizationCodeLifetime = entity.AuthorizationCodeLifetime;
            clientDto.ConsentLifetime = (int)entity.ConsentLifetime;
            clientDto.IdentityTokenLifetime = entity.IdentityTokenLifetime;
            clientDto.SlidingRefreshTokenLifetime = entity.SlidingRefreshTokenLifetime;

            return clientDto;
        }

    }
}
