using Data.Interfaces;
using Dto.Subject.Request;
using Dto.Subject.Return;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class SubjectData : ISubjectData
{
    private readonly ContextInter Db;

    public SubjectData(ContextInter db)
    {
        Db = db;
    }

    public async Task<SubjectReturnDto> CreateAsync(CreateSubjectRequestDto dto)
    {
        var subject = new Subject
        {
            Name = dto.Name,
            Credits = dto.Credits
        };

        Db.Subjects.Add(subject);
        await Db.SaveChangesAsync();

        return new SubjectReturnDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Credits = subject.Credits

        };
    }
    public async Task<List<SubjectReturnDto>> GetAllAsync()
    {
        return await Db.Subjects
            .Select(s => new SubjectReturnDto
            {
                Id = s.Id,
                Name = s.Name,
                Credits = s.Credits
            })
            .ToListAsync();
    }

    public async Task<SubjectReturnDto?> GetByIdAsync(int id)
    {
        var s = await Db.Subjects
            .FirstOrDefaultAsync(x => x.Id == id);

        if (s == null) return null;

        return new SubjectReturnDto
        {
            Id = s.Id,
            Name = s.Name,
            Credits = s.Credits
        };
    }

    public async Task<bool> UpdateAsync(int id, CreateSubjectRequestDto dto)
    {
        var subject = await Db.Subjects.FindAsync(id);

        if (subject == null) return false;

        subject.Name = dto.Name;
        subject.Credits = dto.Credits;

        Db.Subjects.Update(subject);
        await Db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exist = await Db.ProfessorSubjects.AnyAsync(x => x.SubjectId == id) || await Db.StudentSubjects.AnyAsync(x => x.SubjectId == id);
        if(exist) return false;

        var subject = await Db.Subjects.FindAsync(id);

        if (subject == null)
            return false;

        Db.Subjects.Remove(subject);
        await Db.SaveChangesAsync();

        return true;
    }
}
