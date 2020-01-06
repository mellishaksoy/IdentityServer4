using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.API.Application.Dto.User;
using IdentityServer.API.Models;

namespace IdentityServer.API.Domain
{
    public interface IUserRepository 
    {
        //Task AddAsync(ApplicationUser entity);
        //Task UpdateAsync(ApplicationUser entity);
        //Task DeleteAsync(ApplicationUser entity);
        //Task DeleteBulk(ICollection<ApplicationUser> entity);
        //Task<List<ApplicationUser>> GetAllAsync(Guid tenantId);
        Task<bool> IsUserExistsByMail(string mailAddress, Guid dtoId);
    }
}
