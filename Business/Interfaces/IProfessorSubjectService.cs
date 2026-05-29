using Dto.ProfessorSubject.Request;
using Entities;

namespace Business.Interfaces;

public interface IProfessorSubjectService
{
    Task<ProfessorSubject> CreateAsync(CreateProfessorSubjectRequestDto dto);
    Task<List<ProfessorSubject>> GetAsync(int professorId);
    Task<bool> DeleteAsync(int professorId, int subjectId);
}
