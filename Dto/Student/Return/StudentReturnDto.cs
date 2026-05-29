
namespace Dto.Student.Return;

public record StudentReturnDto
{
    public int Id { get; set; }
    public int Dni { get; set; }
    public string Name { get; set; } = string.Empty;
}
