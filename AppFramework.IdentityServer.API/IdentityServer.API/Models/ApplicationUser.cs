using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.API.Models
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public ApplicationUser(string userName)
        {
            this.UserName = userName;
           
        }
        public ApplicationUser()
        {
        }

        

        public Guid CompanyId { get; set; }
        public Guid TenantId { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }

    }

    public interface IEntity
    {
    }
}
