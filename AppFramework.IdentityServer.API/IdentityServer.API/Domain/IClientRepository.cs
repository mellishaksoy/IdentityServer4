
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Domain
{
    public interface IClientRepository
    {
        Task AddAsync(List<Client> entities);
        Task AddAsync(Client entity);
        Task UpdateAsync(List<Client> entities);
        Task UpdateAsync(Client entity);
        Task DeleteAsync(Client entity);
        Task DeleteBulk(ICollection<Client> entity);
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByClientIdAsync(string clientId);
        Task<bool> IsClientExistByClientId(string clientId);

        List<ClientScope> GetScopesNotBelongsToClientId(string clientId);

    }
}
