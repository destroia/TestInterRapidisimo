using Business.Interfaces;
using Data.Interfaces;
using Dto.Subject.Request;
using Dto.Subject.Return;

namespace Business.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectData _subjectData;

    public SubjectService(ISubjectData subjectData)
    {
        _subjectData = subjectData;
    }

    public async Task<SubjectReturnDto> CreateAsync(CreateSubjectRequestDto dto)
    {
        return await _subjectData.CreateAsync(dto);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _subjectData.DeleteAsync(id);
    }

    public async Task<List<SubjectReturnDto>> GetAllAsync()
    {
        return await _subjectData.GetAllAsync();
    }

    public async Task<SubjectReturnDto?> GetByIdAsync(int id)
    {
        return await _subjectData.GetByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(int id, CreateSubjectRequestDto dto)
    {
        return await _subjectData.UpdateAsync(id, dto);
    }
}
