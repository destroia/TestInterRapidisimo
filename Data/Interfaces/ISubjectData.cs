using Dto.Subject.Request;
using Dto.Subject.Return;

namespace Data.Interfaces;

public interface ISubjectData
{
    Task<SubjectReturnDto> CreateAsync(CreateSubjectRequestDto dto);
    Task<List<SubjectReturnDto>> GetAllAsync();
    Task<SubjectReturnDto?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, CreateSubjectRequestDto dto);
    Task<bool> DeleteAsync(int id);
}
