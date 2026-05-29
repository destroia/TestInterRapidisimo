using Data.Interfaces;
using Dto.StudentSubject.Request;
using Dto.StudentSubject.Return;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StudentSubjectData : IStudentSubjectData
{
    private readonly ContextInter Db;

    public StudentSubjectData(ContextInter db)
    {
        Db = db;
    }

    public async Task<StudentSubjectReturnDto> CreateAsync(CreateStudentSubjectRequestDto dto)
    {
        var studentSubject = new StudentSubject { 
            StudentId = dto.StudentId, 
            SubjectId = dto.SubjectId 
        };

        Db.StudentSubjects.Add(studentSubject);

        await Db.SaveChangesAsync();

        return new StudentSubjectReturnDto
        {
            StudentId = studentSubject.StudentId,
            SubjectId = studentSubject.SubjectId
        };
    }

    public async Task<List<StudentSubjectReturnDto>> GetByStudentIdAsync(int studentId)
    {
        return await Db.StudentSubjects
            .Where(ss => ss.StudentId == studentId)
            .Select(s => new StudentSubjectReturnDto
            {
                StudentId = s.StudentId,
                SubjectId = s.SubjectId
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteByStudentIdAsync(int studentId,int SubjectId)
    {
        var existing = await Db.StudentSubjects
            .Where(x => x.StudentId == studentId && x.SubjectId == SubjectId)
            .ToListAsync();

        if (!existing.Any()) return false;

        Db.StudentSubjects.RemoveRange(existing);
        await Db.SaveChangesAsync();

        return true;
    }
}
