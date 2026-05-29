using Dto.ProfessorSubject.Request;
using Entities;

namespace Data.Interfaces;

public interface IProfessorSubjectData
{
    Task<ProfessorSubject> CreateAsync(CreateProfessorSubjectRequestDto dto);
    Task<List<ProfessorSubject>> GetAsync(int professorId);
    Task<bool> DeleteAsync(int professorId, int subjectId);
}
