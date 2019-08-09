using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCoreBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreBackEnd.DAL
{
    public class StudentContext:DbContext
    {
        
        public StudentContext(DbContextOptions<StudentContext> options)
        {
            this.context = options;
        }
        public DbContextOptions<StudentContext> context { get; set; }
        public DbSet<StudentData> Students { get; set; }
        public DbSet<SubjectData> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubjectData>()
                .HasKey(data => new{data.StudentDataId, data.SubjectDataId});
            /*modelBuilder.Entity<StudentSubjectData>()
                .HasOne(data => data.StudentData)
                .WithMany(s=>s.StudentSubjectData)
                .HasForeignKey(k=>k.StudentId);
            modelBuilder.Entity<StudentSubjectData>()
                .HasOne(data=>data.SubjectData)
                .WithMany(s=>s.StudentSubjectData)
                .HasForeignKey(k=>k.SubjectId);*/
        }
    }

    public class StudentData
    {
        public string StudentDataId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public ICollection<StudentSubjectData> StudentSubjectData { get; set; } = new List<StudentSubjectData>();
    }

    public class SubjectData
    {
        public string SubjectDataId { get; set; }
        public string Name { get; set; }
        public ICollection<StudentSubjectData> StudentSubjectData { get; set; } = new List<StudentSubjectData>();
    }

    public class StudentSubjectData
    {
        public string StudentDataId { get; set; }
        public StudentData StudentData { get; set; }
        public string SubjectDataId { get; set; }
        public SubjectData SubjectData { get; set; }
    }
       
}