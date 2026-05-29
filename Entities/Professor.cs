namespace Entities;

public record Professor
{
    public int Id { get; set; }
    public int Dni { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ProfessorSubject> ProfessorSubjects { get; set; } = new List<ProfessorSubject>();
}
