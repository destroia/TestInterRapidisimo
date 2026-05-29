using Business.Interfaces;
using Dto.ProfessorSubject.Request;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class ProfessorSubjectsController : ControllerBase
{
    private readonly IProfessorSubjectService _service;

    public ProfessorSubjectsController(IProfessorSubjectService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ProfessorSubject>> Create([FromBody] CreateProfessorSubjectRequestDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet("{professorId}")]
    public async Task<ActionResult<List<ProfessorSubject>>> Get(int professorId)
    {
        var res = await _service.GetAsync(professorId);
        return Ok(res);
    }

    [HttpDelete("{professorId}/{subjectId}")]
    public async Task<IActionResult> Delete(int professorId, int subjectId)
    {
        var ok = await _service.DeleteAsync(professorId, subjectId);
        if (!ok) return NotFound();
        return NoContent();
    }
}
