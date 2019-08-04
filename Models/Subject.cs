namespace DotNetCoreBackEnd.Models
{
    public class Subject
    {
        public Subject(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string GetId()
        {
            return this.Id;
        }
        
        public string GetName()
        {
            return this.Name;
        }
        
        private string Id { get; }
        private string Name { get; }
    }
}