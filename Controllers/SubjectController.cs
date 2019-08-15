using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreBackEnd.Models;
using DotNetCoreBackEnd.Models.DotNetCoreBackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {

        private readonly IRepository _repository;

        public SubjectController(IRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetAllSubjects()
        {
            List<Subject> subjects = (await _repository.GetAllSubjects()).ToList();
            return Ok(subjects);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(string id)
        {
            Subject subject = await _repository.GetSubject(id);
            return Ok(subject);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject([FromBody] SubjectRequest subject)
        {
            await _repository.AddSubject(new Subject(subject.Id, subject.Name, new List<StudentDomain>()));
            return Created(Request.Path.Value + "/" + subject.Id, subject);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Subject>> PutStudent(string id, [FromBody] SubjectRequest subject)
        {
            var students = (await _repository.GetSubject(id)).Students;
            await _repository.DeleteSubject(id);
            await _repository.AddSubject(new Subject(subject.Id, subject.Name, students));
            return Ok(new Subject(subject.Id, subject.Name, students));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _repository.DeleteSubject(id);
        }
    }
}
