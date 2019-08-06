using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreBackEnd.DAL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace DotNetCoreBackEnd.Models
{
    public class Repository: IRepository
    {
        private StudentContext _database;
        public Repository(StudentContext database)
        {
            this._database = database;
        }
        
        public async Task<IEnumerable<StudentDomain>> GetAllStudent()
        {
            return (await _database.Students.ToListAsync()).Select(DataToDomain);
        }

        public async Task<StudentDomain> GetStudent(Guid id)
        {
            var result = await _database.Students.Where(s => s.Id == id.ToString()).ToListAsync();
            
            List<StudentData> studentDatas = new List<StudentData>();
            List<Subject> subjects = new List<Subject>();

            foreach (StudentData studentData in result)
            {
                foreach (StudentSubjectData studentSubjectData in studentData.StudentSubjectData)
                {
                    subjects.Add(new Subject(studentSubjectData.SubjectData.Id, studentSubjectData.SubjectData.Name));
                }
                studentDatas.Add(studentData);
            }

            StudentDomain studentDomain = new StudentDomain(
                Guid.Parse(studentDatas[0].Id),
                studentDatas[0].Name,
                Int32.Parse(studentDatas[0].Age),
                subjects
            );

            return studentDomain;
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