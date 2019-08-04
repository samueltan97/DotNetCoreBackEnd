using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreBackEnd.Models
{
    public class Repository: IRepository
    {
        public Task<StudentDomain> DataToDomain(StudentData studentData)
        {
            throw new NotImplementedException();
        }

        public Task<StudentData> DomainToData(StudentDomain studentDomain)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDomain>> GetAllStudent()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDomain> GetStudent(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDomain> AddStudent(StudentDomain studentDomain)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDomain> PutStudent(StudentDomain studentDomain)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudent(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}