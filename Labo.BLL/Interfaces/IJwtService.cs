namespace Labo.BLL.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(string identifier, string email, string role);
    }
}