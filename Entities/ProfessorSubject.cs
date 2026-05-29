namespace Entities;

public record ProfessorSubject
{
    public int ProfessorId { get; set; }
    public int SubjectId { get; set; }
    public Professor Professor { get; set; } = null!;
    public Subject Subject { get; set; } = null!;
}
