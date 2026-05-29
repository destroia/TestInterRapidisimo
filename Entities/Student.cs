namespace Entities;

public record Student
{
    public int Id { get; set; }
    public int Dni { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
}
