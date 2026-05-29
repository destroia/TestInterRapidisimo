namespace Business.Interfaces;

public interface IBeginSessionService
{
    string GenerateToken(int adminId, string login);
}
