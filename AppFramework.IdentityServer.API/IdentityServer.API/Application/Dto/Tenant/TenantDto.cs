using System;

namespace IdentityServer.API.Application.Dto.Tenant
{
    public class TenantDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string CseId { get; set; }
        public string CseName { get; set; }
    }

    public static class  Tenant
    {
        public static string Uri { get; set; }
    }


}
