using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace DotNetCoreBackEnd.Models
{
    public class Repository: IRepository
    {
        private DbContext _database;
        public Repository(DbContext database)
        {
            this._database = database;
        }
        
        public Task<IEnumerable<StudentDomain>> GetAllStudent()
        {
            return _database.
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
        
        public StudentDomain DataToDomain(StudentData studentData)
        {
            IEnumerable<Subject> subjects = studentData.GetSubjects().ToList().Select(StringToSubject);
            
            StudentDomain studentDomain = new StudentDomain(
                Guid.Parse(studentData.GetId()),
                studentData.GetName(),
                Int32.Parse(studentData.GetAge()),
                subjects
            );

            return studentDomain;
        }

        public StudentData DomainToData(StudentDomain studentDomain)
        {
            string[] subjects = new string[]{};
            studentDomain.GetSubjects().Select(x => subjects.Append(SubjectToString(x)));
            StudentData studentData = new StudentData(
            studentDomain.GetId().ToString(),
                studentDomain.GetName(),
           studentDomain.GetAge().ToString(),
                subjects
                );
            return studentData;
        }

        private Subject StringToSubject(string subjectSting)
        {
            return JsonConvert.DeserializeObject<Subject>(subjectSting);
        }
        
        private string SubjectToString(Subject subject)
        {
            return JsonConvert.SerializeObject(subject);
        }
    }
}