using Dto.StudentSubject.Request;
using Dto.StudentSubject.Return;

namespace Data.Interfaces;

public interface IStudentSubjectData
{
    Task<StudentSubjectReturnDto> CreateAsync(CreateStudentSubjectRequestDto dto);
    Task<List<StudentSubjectReturnDto>> GetByStudentIdAsync(int studentId);
    Task<bool> DeleteByStudentIdAsync(int studentId, int subjectId);
}
