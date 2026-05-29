using Business.Interfaces;
using Data.Interfaces;
using Data.Repositories;
using Dto.Professor.Request;
using Dto.Professor.Return;
using Dto.Subject.Request;

namespace Business.Services;

public class ProfessorService : IProfessorService
{
    private readonly IProfessorData _professorData;

    public ProfessorService(IProfessorData professorData)
    {
        _professorData = professorData;
    }

    public async Task<ProfessorReturnDto> CreateAsync(CreateProfessorRequestDto dto)
    {
        return await _professorData.CreateAsync(dto);
    }
    public async Task<bool> UpdateAsync(int id, CreateProfessorRequestDto dto)
    {
        return await _professorData.UpdateAsync(id, dto);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _professorData.DeleteAsync(id);
    }

    public async Task<List<ProfessorReturnDto>> GetAllAsync()
    {
        return await _professorData.GetAllAsync();
    }

    public async Task<ProfessorReturnDto?> GetByIdAsync(int id)
    {
        return await _professorData.GetByIdAsync(id);
    }
}
