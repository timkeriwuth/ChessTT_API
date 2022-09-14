using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
using Labo.DL.Entities;
using System.Security.Authentication;
using ToolBox.Security.Utils;

namespace Labo.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository, IMailer mailer)
        {
            _userRepository = userRepository;
        }

        public UserDTO Login(LoginDTO dto)
        {
            User? user = _userRepository.FindOne(u => !u.IsDeleted && (u.Username.ToLower() == dto.Username.ToLower() || u.Email.ToLower() == dto.Username.ToLower()));
            if (user is null || !HashUtils.VerifyPassword(user.EncodedPassword, dto.Password, user.Salt))
            {
                throw new AuthenticationException();
            }
            return new UserDTO(user);
        }
    }
}
