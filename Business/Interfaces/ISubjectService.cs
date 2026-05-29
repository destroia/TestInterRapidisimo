using Dto.Subject.Request;
using Dto.Subject.Return;

namespace Business.Interfaces;

public interface ISubjectService
{
    Task<SubjectReturnDto> CreateAsync(CreateSubjectRequestDto dto);
    Task<List<SubjectReturnDto>> GetAllAsync();
    Task<SubjectReturnDto?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, CreateSubjectRequestDto dto);
    Task<bool> DeleteAsync(int id);
}
