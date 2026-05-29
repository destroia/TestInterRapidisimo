using Data.Interfaces;
using Dto.Professor.Request;
using Dto.Professor.Return;
using Dto.Subject.Request;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProfessorData : IProfessorData
{
    private readonly ContextInter Db;

    public ProfessorData(ContextInter db)
    {
        Db = db;
    }

    public async Task<ProfessorReturnDto> CreateAsync(CreateProfessorRequestDto dto)
    {
        var professor = new Professor
        {
            Dni = dto.Dni,
            Name = dto.Name
        };

        Db.Professors.Add(professor);

        await Db.SaveChangesAsync();

        return new ProfessorReturnDto
        {
            Id = professor.Id,
            Dni = professor.Dni,
            Name = professor.Name,
            Subjects = new List<string>()
        };
    }

    public async Task<List<ProfessorReturnDto>> GetAllAsync()
    {
        return await Db.Professors
            .Include(p => p.ProfessorSubjects)
            .Select(p => new ProfessorReturnDto
            {
                Id = p.Id,
                Dni = p.Dni,
                Name = p.Name,
                Subjects = p.ProfessorSubjects.Select(s => s.Subject.Name).ToList()
            })
            .ToListAsync();
    }

    public async Task<ProfessorReturnDto?> GetByIdAsync(int id)
    {
        var p = await Db.Professors
            .Include(x => x.ProfessorSubjects)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (p == null) return null;

        return new ProfessorReturnDto
        {
            Id = p.Id,
            Dni = p.Dni,
            Name = p.Name,
            Subjects = p.ProfessorSubjects.Select(s => s.Subject.Name).ToList()
        };
    }
    public async Task<bool> UpdateAsync(int id, CreateProfessorRequestDto dto)
    {
        var professor = await Db.Professors.FindAsync(id);

        if (professor == null) return false;

        professor.Dni = dto.Dni;
        professor.Name = dto.Name;

        Db.Professors.Update(professor);
        await Db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(await Db.ProfessorSubjects.AnyAsync(x => x.ProfessorId == id))
            return false;

        var professor = await Db.Professors.FindAsync(id);

        if (professor == null)
            return false;

        Db.Professors.Remove(professor);

        await Db.SaveChangesAsync();

        return true;
    }
}
