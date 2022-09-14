using Labo.BLL.DTO.Users;

namespace Labo.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        UserDTO Login(LoginDTO dto);
    }
}