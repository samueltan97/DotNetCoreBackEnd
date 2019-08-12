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
        
        /*private Dictionary<string, StudentData> studentDatabase;
        private Dictionary<string, SubjectData> subjectDatabase;*/
        /*public Repository()
        {
            this._database = database;
            /*this.studentDatabase = new Dictionary<string, StudentData>();
            this.subjectDatabase = new Dictionary<string, SubjectData>();#1#
        }*/
        
        public async Task<IEnumerable<StudentDomain>> GetAllStudent()
        {
            var students = await _database.Students
                .Include(student => student.StudentSubjectData)
                .ThenInclude(data => data.SubjectData)
                .ToListAsync();
            List<StudentDomain> studentDomain = new List<StudentDomain>();
            foreach (var student in students)
            {
                List<Subject> subjects = new List<Subject>();
                foreach (var subject in student.StudentSubjectData.Select(data=>data.SubjectData))
                {
                    subjects.Add(new Subject(subject.SubjectDataId, subject.Name, null));
                }
                studentDomain.Add(new StudentDomain(
                    Guid.Parse(student.StudentDataId), 
                    student.Name, 
                    Int32.Parse(student.Age), 
                    subjects) );
            }
            return studentDomain;
            /*var list = new List<StudentDomain>();
            foreach (var student in studentDatabase.Values)
            {
                list.Add(new StudentDomain(Guid.Parse(student.StudentDataId), student.Name, Int32.Parse(student.Age), 
                        null
                    ));
            }
            return list;*/
        }

        public async Task<StudentDomain> GetStudent(Guid id)
        {
            var result = await _database.Students
                .Where(s => s.StudentDataId == id.ToString())
                .Include(student => student.StudentSubjectData)
                .ThenInclude(studentSubjectData => studentSubjectData.SubjectData)
                .SingleAsync();
            
            List<Subject> subjects = new List<Subject>();

            foreach (StudentSubjectData studentSubjectData in result.StudentSubjectData)
            {
                subjects.Add(new Subject(studentSubjectData.SubjectData.SubjectDataId, studentSubjectData.SubjectData.Name, null
                ));
            }

            StudentDomain studentDomain = new StudentDomain(
                Guid.Parse(result.StudentDataId),
                result.Name,
                Int32.Parse(result.Age),
                subjects
            );

            return studentDomain;
            
            /*return new StudentDomain(id, studentDatabase[id.ToString()].Name, Int32.Parse(studentDatabase[id.ToString()].Age),
                    null
                );*/
        }

        public async Task AddStudent(StudentDomain studentDomain)
        {

            StudentData studentData = new StudentData
            {
                StudentDataId = studentDomain.Id.ToString(),
                Age = studentDomain.Age.ToString(),
                Name = studentDomain.Name,
            };
            
            foreach (var subject in studentDomain.Subjects)
            {
                studentData.StudentSubjectData.Add(new StudentSubjectData
                {
                    StudentDataId = studentDomain.Id.ToString(),
                    StudentData = studentData,
                    SubjectDataId = subject.Id,
                    SubjectData = await _database.Subjects.SingleAsync(item=>item.SubjectDataId == subject.Id)
                });
            }

            await _database.Students.AddAsync(studentData);
            _database.SaveChanges();

            /*studentDatabase.Add(studentDomain.Id.ToString(), new StudentData {
                StudentDataId = studentDomain.Id.ToString(),
                Name = studentDomain.Name,
                Age = studentDomain.Age.ToString()
            });*/
    }

        public async Task DeleteStudent(Guid id)
        {
            var student = await _database.Students.SingleAsync(studentData => studentData.StudentDataId == id.ToString());
            student.StudentSubjectData.Clear();
            _database.Students.Remove(student);
            _database.SaveChanges();

/*
            studentDatabase.Remove(id.ToString());
*/
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            var subjects = await _database.Subjects.Include(subject => subject.StudentSubjectData)
                .ThenInclude(data => data.StudentData)
                .ToListAsync();
            
            List<Subject> subjectList = new List<Subject>();

            foreach (var subject in subjects)
            {
                List<StudentDomain> students = new List<StudentDomain>();
                foreach (var student in subject.StudentSubjectData.Select(s=>s.StudentData))
                {
                    students.Add(new StudentDomain(
                        Guid.Parse(student.StudentDataId), 
                        student.Name, 
                        Int32.Parse(student.Age),
                            null
                        ));
                }
                subjectList.Add(new Subject(subject.SubjectDataId, subject.Name, students));
            }

            return subjectList;
        }

        public async Task<Subject> GetSubject(string id)
        {
            var foundSubject = await _database.Subjects.Include(subject => subject.StudentSubjectData)
                .ThenInclude(data => data.StudentData)
                .SingleAsync(chosenSubject => chosenSubject.SubjectDataId == id);
            
            List<StudentDomain> students = new List<StudentDomain>();

            foreach (var student in foundSubject.StudentSubjectData.Select(data=>data.StudentData))
            {
                students.Add(new StudentDomain(Guid.Parse(student.StudentDataId), student.Name, Int32.Parse(student.Age), null));
            }
            
            return  new Subject(foundSubject.SubjectDataId, foundSubject.Name, students);
        }

        public async Task AddSubject(Subject subject)
        {
            var subjectData = new SubjectData
            {
                SubjectDataId = subject.Id,
                Name = subject.Name
            };

            foreach (var student in subject.Students)
            {
                var studentData = await _database.Students.SingleAsync(data => data.StudentDataId == student.Id.ToString());
                subjectData.StudentSubjectData.Add(new StudentSubjectData
                {
                    StudentDataId = studentData.StudentDataId,
                    StudentData = studentData,
                    SubjectDataId = subject.Id,
                    SubjectData = subjectData
                });
            }

            await _database.Subjects.AddAsync(subjectData);
            _database.SaveChanges();
        }


        public async Task DeleteSubject(string id)
        {
            var subject = await _database.Subjects.Include(subjectData => subjectData.StudentSubjectData)
                .ThenInclude(data => data.StudentData)
                .SingleAsync(s => s.SubjectDataId == id);
            
            subject.StudentSubjectData.Clear();
            _database.Subjects.Remove(subject);
            _database.SaveChanges();
        }
    }
}