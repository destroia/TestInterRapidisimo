namespace Dto.Subject.Request;

public record CreateSubjectRequestDto
{
    public string Name { get; set; } = string.Empty;
    public int Credits { get; set; } = 3;
}
