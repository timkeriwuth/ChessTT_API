namespace Labo.IL.Services
{
    public interface IJwtService
    {
        string CreateToken(string identifier, string email, string role);
    }
}