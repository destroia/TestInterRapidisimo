using Dto.StudentSubject.Request;
using Dto.StudentSubject.Return;

namespace Business.Interfaces;

public interface IStudentSubjectService
{
    Task<StudentSubjectReturnDto> CreateAsync(CreateStudentSubjectRequestDto dto);
    Task<List<StudentSubjectReturnDto>> GetByStudentIdAsync(int studentId);
    Task<bool> DeleteByStudentIdAsync(int studentId, int SubjectId);
}
