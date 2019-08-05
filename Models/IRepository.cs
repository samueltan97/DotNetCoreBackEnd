using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreBackEnd.Models
{
    public interface IRepository
    { 
        Task<IEnumerable<StudentDomain>> GetAllStudent();
        Task<StudentDomain> GetStudent(Guid id);
        Task<StudentDomain> AddStudent(StudentDomain studentDomain);
        Task<StudentDomain> PutStudent(StudentDomain studentDomain);
        Task DeleteStudent(Guid id);
    }
}