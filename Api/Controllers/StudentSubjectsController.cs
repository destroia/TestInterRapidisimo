using Business.Interfaces;
using Dto.StudentSubject.Request;
using Dto.StudentSubject.Return;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StudentSubjectsController : ControllerBase
    {
        private readonly IStudentSubjectService _service;
        public StudentSubjectsController(IStudentSubjectService service)
        {
            _service = service;
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<List<StudentSubjectReturnDto>>> GetByStudentId(int studentId)
        {
            var studentSubjects = await _service.GetByStudentIdAsync(studentId);
            return Ok(studentSubjects);
        }

        [HttpPost]
        public async Task<ActionResult<StudentSubjectReturnDto>> Create(CreateStudentSubjectRequestDto dto)
        {
            var studentSubject = await _service.CreateAsync(dto);
            return Ok(studentSubject);
        }

        [HttpDelete("{studentId}/{subjectId}")]
        public async Task<ActionResult<bool>> Delete(int studentId, int subjectId)
        {
            var result = await _service.DeleteByStudentIdAsync(studentId, subjectId);
            return Ok(result);
        }
    }
}
