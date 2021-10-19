namespace Employee.Portal.Service.Interfaces
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
        bool Validate(string refreshToken);
    }
}
