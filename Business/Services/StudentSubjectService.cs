using Business.Interfaces;
using Data.Interfaces;
using Dto.StudentSubject.Request;
using Dto.StudentSubject.Return;

namespace Business.Services;

public class StudentSubjectService : IStudentSubjectService
{
    private readonly IStudentSubjectData _studentSubjectData;
    public StudentSubjectService(IStudentSubjectData studentSubjectData)
    {
       _studentSubjectData = studentSubjectData;
    }
    public Task<StudentSubjectReturnDto> CreateAsync(CreateStudentSubjectRequestDto dto)
    {
        return _studentSubjectData.CreateAsync(dto);
    }

    public Task<bool> DeleteByStudentIdAsync(int studentId, int SubjectId)
    {
        return _studentSubjectData.DeleteByStudentIdAsync(studentId, SubjectId);
    }

    public Task<List<StudentSubjectReturnDto>> GetByStudentIdAsync(int studentId)
    {
        return _studentSubjectData.GetByStudentIdAsync(studentId);
    }
}
