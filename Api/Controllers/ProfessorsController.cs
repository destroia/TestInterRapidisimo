using Business.Interfaces;
using Dto.Professor.Request;
using Dto.Professor.Return;
using Dto.Subject.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class ProfessorsController : ControllerBase
{
    private readonly IProfessorService _service;

    public ProfessorsController(IProfessorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProfessorReturnDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfessorReturnDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProfessorReturnDto>> Create(CreateProfessorRequestDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateProfessorRequestDto dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        return Ok(await _service.DeleteAsync(id));   
    }
}
