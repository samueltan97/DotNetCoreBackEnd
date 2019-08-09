using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreBackEnd.DAL;

namespace DotNetCoreBackEnd.Models
{
    public interface IRepository
    { 
        Task<IEnumerable<StudentDomain>> GetAllStudent();
        Task<StudentDomain> GetStudent(Guid id);
        Task AddStudent(StudentDomain studentDomain);
        Task DeleteStudent(Guid id);
        
        /*Task<IEnumerable<Subject>> GetAllSubjects();
        Task<Subject> GetSubject(string id);
        Task AddSubject(Subject subject);
        Task DeleteSubject(string id);*/
        
        
    }
}