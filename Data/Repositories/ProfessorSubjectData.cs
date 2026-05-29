using Data.Interfaces;
using Dto.ProfessorSubject.Request;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProfessorSubjectData : IProfessorSubjectData
{
    private readonly ContextInter Db;

    public ProfessorSubjectData(ContextInter db)
    {
        Db = db;
    }

    public async Task<ProfessorSubject> CreateAsync(CreateProfessorSubjectRequestDto dto)
    {
        var professor = await Db.Professors.FindAsync(dto.ProfessorId);
        if (professor == null) throw new KeyNotFoundException("Profesor no encontrado.");

        var subject = await Db.Subjects.FindAsync(dto.SubjectId);
        if (subject == null) throw new KeyNotFoundException("Materia no encontrada.");

        var entity = new ProfessorSubject
        {
            ProfessorId = dto.ProfessorId,
            SubjectId = dto.SubjectId
        };

        Db.ProfessorSubjects.Add(entity);
        await Db.SaveChangesAsync();

        return entity;
    }

    public async Task<List<ProfessorSubject>> GetAsync(int professorId)
    {
        var professorSubjects = await Db.ProfessorSubjects
            //.Include(ps => ps.Subject)
            .Where(ps => ps.ProfessorId == professorId)
            .ToListAsync();
        return professorSubjects;
    }

    public async Task<bool> DeleteAsync(int professorId, int subjectId)
    {
        var existing = await Db.ProfessorSubjects.Where(ps => ps.ProfessorId == professorId && ps.SubjectId == subjectId).ToListAsync();
        if (!existing.Any()) return false;

        Db.ProfessorSubjects.RemoveRange(existing);
        await Db.SaveChangesAsync();

        return true;
    }
}
