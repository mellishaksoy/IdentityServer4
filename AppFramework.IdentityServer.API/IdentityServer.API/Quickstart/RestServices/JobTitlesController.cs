using IdentityServer.API.Application.Services.JobTitle;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Quickstart.RestServices
{
    [Route("api/v1/[controller]")]
    public class JobTitlesController : Controller
    {
        private readonly IJobTitleService _jobTitleService;
        public JobTitlesController(IJobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;

        }
        
    }
}