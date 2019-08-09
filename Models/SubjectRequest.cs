using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreBackEnd.Models
{
    namespace DotNetCoreBackEnd.Models
    {
        public class SubjectRequest
        {
            public SubjectRequest(string name, StudentDomain[] students)
            {
                this.Name = name;
                this.Students = students.ToList();
            }
        
            public string Name { get; }
            public List<StudentDomain> Students { get; }
        }
    }
}