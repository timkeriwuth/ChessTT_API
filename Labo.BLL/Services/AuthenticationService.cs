using Labo.BLL.DTO.Users;
using Labo.BLL.Exceptions;
using Labo.BLL.Mappers;
using Labo.DAL.Repositories;
using Labo.DL.Entities;
using Labo.DL.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using ToolBox.Security.Utils;

namespace Labo.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(RegisterDTO dto)
        {
            if(_userRepository.Any(u => u.Username.ToLower() == dto.Username.ToLower()))
            {
                throw new ValidationException("This username already exists") { Source = nameof(dto.Username) };
            }
            User u = dto.ToEntity();
            u.Elo = dto.Elo ?? 1200;
            u.Role = UserRole.Player;
            u.Salt = Guid.NewGuid();
            u.EncodedPassword = HashUtils.HashPassword(dto.Password, u.Salt);
            u.IsDeleted = false;
            _userRepository.Add(u);
        }

        public UserDTO Login(LoginDTO dto)
        {
            User? user = _userRepository.FindOne(u => !u.IsDeleted && u.Username.ToLower() == dto.Username.ToLower());
            if(user is null || !HashUtils.VerifyPassword(user.EncodedPassword, dto.Password, user.Salt))
            {
                throw new AuthenticationException();
            }
            return new UserDTO(user);
        }
    }
}
