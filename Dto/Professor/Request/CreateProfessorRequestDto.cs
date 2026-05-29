namespace Dto.Professor.Request;

public record CreateProfessorRequestDto
{
    public int Dni { get; set; }
    public string Name { get; set; } = string.Empty;
}
