using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCoreBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreBackEnd.DAL
{
    public class StudentContext:DbContext
    {
        public DbSet<StudentData> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubjectData>()
                .HasKey(data => new{data.StudentId, data.SubjectId});
            modelBuilder.Entity<StudentSubjectData>()
                .HasOne(data => data.StudentData)
                .WithMany(s=>s.StudentSubjectData)
                .HasForeignKey(k=>k.StudentId);
            modelBuilder.Entity<StudentSubjectData>()
                .HasOne(data=>data.SubjectData)
                .WithMany(s=>s.StudentSubjectData)
                .HasForeignKey(k=>k.SubjectId);
        }
    }

    public class StudentData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public ICollection<StudentSubjectData> StudentSubjectData { get; set; }
    }

    public class SubjectData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentSubjectData> StudentSubjectData { get; set; }
    }

    public class StudentSubjectData
    {
        public string StudentId { get; set; }
        public StudentData StudentData { get; set; }
        public string SubjectId { get; set; }
        public SubjectData SubjectData { get; set; }
    }
       
}