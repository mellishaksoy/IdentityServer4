using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Models
{
    public class ApplicationJobTitleTranslation
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public int JobTitleId { get; set; }
        public string LanguageCode { get; set; }
        public ApplicationJobTitle JobTitle { get; set; }
    }
}
