namespace Entities;

public record StudentSubject
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public Student Student { get; set; } = null!;
    public Subject Subject { get; set; } = null!;
}
