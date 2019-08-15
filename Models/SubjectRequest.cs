using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotNetCoreBackEnd.Models
{
    namespace DotNetCoreBackEnd.Models
    {
        public class SubjectRequest
        {
            [Required] public string Id { get; set; }
            [Required] public string Name { get; set; }
        }
    }
}