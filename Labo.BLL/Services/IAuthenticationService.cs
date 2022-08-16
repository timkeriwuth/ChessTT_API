using Labo.BLL.DTO.Users;

namespace Labo.BLL.Services
{
    public interface IAuthenticationService
    {
        UserDTO Login(LoginDTO dto);
        void Register(RegisterDTO dto);
        void ChangePassword(Guid id, ChangePasswordDTO dto);
        bool ExistsEmail(string email, Guid? id);
        bool ExistsUsername(string username, Guid? id);
    }
}