using Labo.BLL.DTO.Users;

namespace Labo.BLL.Services
{
    public interface IAuthenticationService
    {
        UserDTO Login(LoginDTO dto);
    }
}