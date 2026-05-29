using Dto.Professor.Request;
using Dto.Professor.Return;

namespace Data.Interfaces;

public interface IProfessorData
{
    Task<ProfessorReturnDto> CreateAsync(CreateProfessorRequestDto dto);
    Task<List<ProfessorReturnDto>> GetAllAsync();
    Task<ProfessorReturnDto?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(int id, CreateProfessorRequestDto dto);
}
