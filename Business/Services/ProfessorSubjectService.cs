using Business.Interfaces;
using Data.Interfaces;
using Dto.ProfessorSubject.Request;
using Entities;

namespace Business.Services;

public class ProfessorSubjectService : IProfessorSubjectService
{
    private readonly IProfessorSubjectData _professorSubjectService;
    public ProfessorSubjectService(IProfessorSubjectData professorSubjectService)
    {
        _professorSubjectService = professorSubjectService;
    }
    public async Task<ProfessorSubject> CreateAsync(CreateProfessorSubjectRequestDto dto)
    {
        return await _professorSubjectService.CreateAsync(dto);
    }

    public async Task<bool> DeleteAsync(int professorId, int subjectId)
    {
        return await _professorSubjectService.DeleteAsync(professorId, subjectId);
    }

    public async Task<List<ProfessorSubject>> GetAsync(int professorId)
    {
        return await _professorSubjectService.GetAsync(professorId);
    }
}
