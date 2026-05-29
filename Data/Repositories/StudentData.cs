using Data.Interfaces;
using Dto.Student.Request;
using Dto.Student.Return;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StudentData : IStudentData
{
    private readonly ContextInter Db;

    public StudentData(ContextInter db)
    {
        Db = db;
    }
    public async Task<StudentReturnDto> CreateAsync(CreateStudentRequestDto dto)
    {
        var student = new Student
        {
            Dni = dto.Dni,
            Name = dto.Name
        };

        Db.Students.Add(student);

        await Db.SaveChangesAsync();

        return new StudentReturnDto
        {
            Id = student.Id,
            Dni = student.Dni,
            Name = student.Name
        };
    }
    public async Task<bool> DuplicatedProfessorsAsync(List<int> subjectIds)
    {
        var subjects = await Db.Subjects

            .Where(x => subjectIds.Contains(x.Id))
            .ToListAsync();

        return false;
    }
    public async Task<List<StudentReturnDto>> GetAllAsync()
    {
        return await Db.Students
            .Select(x => new StudentReturnDto
            {
                Id = x.Id,
                Dni = x.Dni,
                Name = x.Name,
            })
            .ToListAsync();
    }

    public async Task<StudentReturnDto?> GetByIdAsync(int id)
    {
        var student = await Db.Students
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student == null)
            return null;

        return new StudentReturnDto
        {
            Id = student.Id,
            Dni = student.Dni,
            Name = student.Name
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(await Db.StudentSubjects.AnyAsync(x => x.StudentId == id))
            return false;
        var student = await Db.Students.FindAsync(id);

        if (student == null)
            return false;

        Db.Students.Remove(student);

        await Db.SaveChangesAsync();

        return true;
    }
}
