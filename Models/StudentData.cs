using System.ComponentModel.DataAnnotations;

namespace DotNetCoreBackEnd.Models
{
    public class StudentData
    {
        public StudentData(string id, string name, string age, string[] subjects)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Subjects = subjects;
        }

        public string GetId()
        {
            return this.Id;
        }
        
        public string GetName()
        {
            return this.Name;
        }
        
        public string GetAge()
        {
            return this.Age;
        }
        
        public string[] GetSubjects()
        {
            return this.Subjects;
        }
        
        [Key]
        private string Id { get; }
        private string Name { get; }
        private string Age { get; }
        private string[] Subjects { get; }
    }
}