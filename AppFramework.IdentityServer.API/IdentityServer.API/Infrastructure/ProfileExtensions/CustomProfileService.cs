using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.API.Application.Dto.Tenant;
using IdentityModel;
using IdentityServer.API.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace IdentityServer.API.Infrastructure.ProfileExtensions
{
    public class CustomProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _clientFactory;

        public CustomProfileService(UserManager<ApplicationUser> userManager,
            IHttpClientFactory clientFactory)
        {
            _userManager = userManager;
            _clientFactory = clientFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

            var existClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                //new Claim(JwtClaimTypes.Email, user.Email),
                //new Claim("FullName", user.FullName),
                //new Claim("tenantId", user.TenantId.ToString())
            };

            if (existClaims != null && existClaims.Any())
                claims = claims.Concat(existClaims).ToList();

            context.IssuedClaims.AddRange(claims);

            foreach (var role in roles)
            {
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
            }

            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            /*var user = await _userManager.GetUserAsync(context.Subject);
      
            context.IsActive = (user != null) && user.d;*/
            //return Task.CompletedTask;

        }

    }

}
