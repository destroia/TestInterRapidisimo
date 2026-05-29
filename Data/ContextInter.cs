using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ContextInter : DbContext
{
    public ContextInter(DbContextOptions<ContextInter> options) : base(options)
    {
    }

    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<ProfessorSubject> ProfessorSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentSubject>()
            .HasKey(ss => new { ss.StudentId, ss.SubjectId });

        modelBuilder.Entity<ProfessorSubject>()
            .HasKey(ps => new { ps.ProfessorId, ps.SubjectId });
    }
}
