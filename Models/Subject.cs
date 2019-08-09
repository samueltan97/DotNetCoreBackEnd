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
            this.Students = students.ToList();
        }

        public string Id { get; }
        public string Name { get; }
        public IEnumerable<StudentDomain> Students { get; }
    }
}