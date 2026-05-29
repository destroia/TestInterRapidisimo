using Dto.Student.Request;
using Dto.Student.Return;

namespace Business.Interfaces;

public interface IStudentService
{
    Task<StudentReturnDto> CreateAsync(CreateStudentRequestDto dto);

    Task<List<StudentReturnDto>> GetAllAsync();

    Task<StudentReturnDto?> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);
}
