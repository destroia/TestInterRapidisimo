namespace Entities;

public record Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Credits { get; set; } = 3;
    public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    public ICollection<ProfessorSubject> ProfessorSubjects { get; set; } = new List<ProfessorSubject>();
}
