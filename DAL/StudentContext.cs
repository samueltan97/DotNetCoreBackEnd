using DotNetCoreBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreBackEnd.DAL
{
    public class StudentContext:DbContext
    {
        public StudentContext() : base("SchoolContext")
        {
        }
        
        public DbSet<StudentData> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}