using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.API.Application.Dto.User;
using IdentityServer.API.Domain;
using IdentityServer.API.Infrastructure.Exceptions;
using IdentityServer.API.Models;
using IdentityServer4.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.API.Application.Services.User
{
    public class UserService 
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new NotImplementedException();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }
        

    }
}
