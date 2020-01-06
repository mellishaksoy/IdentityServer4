using IdentityServer.API.Data;
using IdentityServer.API.Domain;
using IdentityServer.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Application.Repositories
{
    public class JobtitleRepository : IJobtitleRepository
    {
        private readonly ApplicationDbContext _ctx;
        public JobtitleRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}
