namespace Dto.ProfessorSubject.Request;

public record CreateProfessorSubjectRequestDto
{
    public int ProfessorId { get; set; }
    public int SubjectId { get; set; }
}
