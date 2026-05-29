namespace Dto.BeginSession.Request;

public record BeginSessionRequestDto
{
    public int AdminId { get; init; }
    public string Login { get; init; } = string.Empty;
}
