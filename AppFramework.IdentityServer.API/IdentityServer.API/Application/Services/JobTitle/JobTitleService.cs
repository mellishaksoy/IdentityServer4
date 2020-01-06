using IdentityServer.API.Domain;

namespace IdentityServer.API.Application.Services.JobTitle
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobtitleRepository _jobtitleRepository;
        public JobTitleService(IJobtitleRepository jobtitleRepository)
        {
            _jobtitleRepository = jobtitleRepository;
        }
    }
}
