using IdentityServer.API.Application.Dto.User;
using IdentityServer.API.Domain;
using IdentityServer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.API.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.API.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UserRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(ApplicationUser entity)
        {
            _ctx.Users.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationUser entity)
        {
            _ctx.Users.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(ApplicationUser entity)
        {
            _ctx.Users.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteBulk(ICollection<ApplicationUser> entity)
        {
            _ctx.Users.RemoveRange(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<ApplicationUser>> GetAllAsync(Guid tenantId)
        {
            var result = await _ctx.Users.Where(c => c.TenantId == tenantId).ToListAsync();
            return result;
        }
        
        public async Task<ApplicationUser> GetByIdAsync(Guid tenantId, Guid id)
        {
            var result = await _ctx.Users.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            return result;
        }

        public async Task<bool> IsUserExistsByMail(string mailAddress, Guid dtoId)
        {
            var result = await _ctx.Users.FirstOrDefaultAsync(c => c.Email == mailAddress && c.Id != dtoId);
            if (result == null)
                return await Task.FromResult(false);
            else
            {
                return await Task.FromResult(true);
            }

        }

        public async Task<bool> IsUserExistByEmployeeNumber(Guid tenantId, string employeeId, Guid dtoId)
        {
            var result =
                await _ctx.Users.FirstOrDefaultAsync(c => c.EmployeeId == employeeId && c.TenantId == tenantId && c.Id != dtoId);
            if (result == null)
                return await Task.FromResult(false);
            else
                return await Task.FromResult(true);
        }

    }
}