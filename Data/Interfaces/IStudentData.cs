using Dto.Student.Request;
using Dto.Student.Return;

namespace Data.Interfaces;

public interface IStudentData
{
    Task<StudentReturnDto> CreateAsync(CreateStudentRequestDto dto);
    Task<bool> DuplicatedProfessorsAsync(List<int> subjectIds);
    Task<List<StudentReturnDto>> GetAllAsync();
    Task<StudentReturnDto?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}
