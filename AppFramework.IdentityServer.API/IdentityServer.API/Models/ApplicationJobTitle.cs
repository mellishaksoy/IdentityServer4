using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Models
{
    public class ApplicationJobTitle 
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }

        public ICollection<ApplicationJobTitleTranslation> JobTitleTranslations { get; set; }
    }
}
