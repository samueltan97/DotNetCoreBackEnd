using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreBackEnd.Models
{
    public class Subject
    {
        public Subject(string id, string name, IEnumerable<StudentDomain> students)
        {
            this.Id = id;
            this.Name = name;
            this.Students = students;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StudentDomain> Students { get; set; }
    }
}