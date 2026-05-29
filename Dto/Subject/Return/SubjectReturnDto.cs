namespace Dto.Subject.Return;

public record SubjectReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Credits { get; set; } = 3;
}
