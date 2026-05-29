namespace Dto.Student.Request;

public record CreateStudentRequestDto
{
    public int Dni { get; set; }
    public string Name { get; set; } = string.Empty;
}
