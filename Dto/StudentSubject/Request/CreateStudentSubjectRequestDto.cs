namespace Dto.StudentSubject.Request;

public record CreateStudentSubjectRequestDto
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
}
