using Business.Interfaces;
using Data.Interfaces;
using Data.Repositories;
using Dto.Student.Request;
using Dto.Student.Return;

namespace Business.Services;

public class StudentService : IStudentService
{
    private readonly IStudentData _studentData;

    public StudentService(IStudentData studentData)
    {
        _studentData = studentData;
    }
    public async Task<StudentReturnDto> CreateAsync(CreateStudentRequestDto dto)
    {
        var student = await _studentData.CreateAsync(dto);
        return student;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _studentData.DeleteAsync(id);
    }

    public async Task<List<StudentReturnDto>> GetAllAsync()
    {
        return await _studentData.GetAllAsync();
    }

    public async Task<StudentReturnDto?> GetByIdAsync(int id)
    {
        return await _studentData.GetByIdAsync(id);
    }
}
