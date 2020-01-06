using IdentityServer.API.Domain;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Application.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ConfigurationDbContext _ctx;

        public ClientRepository(ConfigurationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task AddAsync(List<Client> entities)
        {
            foreach (var entity in entities)
                _ctx.Clients.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task AddAsync(Client entity)
        {
            _ctx.Clients.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client entity)
        {
            _ctx.Clients.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteBulk(ICollection<Client> entity)
        {
            _ctx.Clients.RemoveRange(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Client> GetByClientIdAsync(string clientId)
        {
            var client = await _ctx.Clients.Include(c => c.AllowedScopes).Include(c => c.ClientSecrets)
                .Include(c => c.RedirectUris).Include(c => c.PostLogoutRedirectUris).Include(c => c.Claims)
                .FirstOrDefaultAsync(c => c.ClientId == clientId && c.Enabled);
            return client;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            var client = await _ctx.Clients.FirstOrDefaultAsync(c => c.Id == id && c.Enabled);
            return client;
        }
        
        public async Task<bool> IsClientExistByClientId(string clientId)
        {
            var client = await _ctx.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId && c.Enabled);
            if (client == null)
                return await Task.FromResult(false);
            else
                return await Task.FromResult(true);
        }

        public async Task UpdateAsync(List<Client> entities)
        {
            _ctx.UpdateRange(entities);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client entity)
        {
            _ctx.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public List<ClientScope> GetScopesNotBelongsToClientId(string clientId)
        {
            var clients = _ctx.Clients.Include(c => c.AllowedScopes).Where(x => x.ClientId != clientId).ToList();
            var scopes = clients.SelectMany(x => x.AllowedScopes).Select(x => x).Distinct().ToList();
            return scopes;
        }
        
    }
}
