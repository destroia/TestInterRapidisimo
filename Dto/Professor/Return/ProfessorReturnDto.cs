namespace Dto.Professor.Return;

public record ProfessorReturnDto
{
    public int Id { get; set; }
    public int Dni { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Subjects { get; set; } = new();
}
