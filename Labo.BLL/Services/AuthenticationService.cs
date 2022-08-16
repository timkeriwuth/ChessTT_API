using Labo.BLL.DTO.Users;
using Labo.BLL.Mappers;
using Labo.DAL.Repositories;
using Labo.DL.Entities;
using Labo.DL.Enums;
using Labo.IL.Services;
using Labo.IL.Utils;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Transactions;
using ToolBox.Security.Utils;

namespace Labo.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailer _mailer;

        private readonly string RegisterMailTemplate = @"
            <h1>Your registration for Mr Checkmate's plateform</h1>
            <div>
                <p>Your Credentials:</p>
                <p>username: __username__</p>
                <p>password: __password__</p>
            </div>
        ";

        public AuthenticationService(IUserRepository userRepository, IMailer mailer)
        {
            _userRepository = userRepository;
            _mailer = mailer;
        }

        public void Register(RegisterDTO dto)
        {
            if(_userRepository.Any(u => u.Username.ToLower() == dto.Username.ToLower()))
            {
                throw new ValidationException("This username already exists") { Source = nameof(dto.Username) };
            }
            if (_userRepository.Any(u => u.Email.ToLower() == dto.Email.ToLower()))
            {
                throw new ValidationException("This email already exists") { Source = nameof(dto.Email) };
            }
            string password = PasswordGenerator.Random(8);
            User u = dto.ToEntity();
            u.Elo = dto.Elo ?? 1200;
            u.Role = UserRole.Player;
            u.Salt = Guid.NewGuid();
            u.EncodedPassword = HashUtils.HashPassword(password, u.Salt);
            u.IsDeleted = false;


            using TransactionScope t = new();  
            _userRepository.Add(u);
            _mailer.Send(
                "New Registration",
                RegisterMailTemplate
                    .Replace("__username__", u.Username)
                    .Replace("__password__", password),
                u.Email // replaced in debug
            );
            t.Complete();
        }

        public UserDTO Login(LoginDTO dto)
        {
            User? user = _userRepository.FindOne(u => !u.IsDeleted && (u.Username.ToLower() == dto.Username.ToLower() || u.Email.ToLower() == dto.Username.ToLower()));
            if(user is null || !HashUtils.VerifyPassword(user.EncodedPassword, dto.Password, user.Salt))
            {
                throw new AuthenticationException();
            }
            return new UserDTO(user);
        }

        public void ChangePassword(Guid id, ChangePasswordDTO dto)
        {
            User? user = _userRepository.FindOne(id);
            if (user is null || !HashUtils.VerifyPassword(user.EncodedPassword, dto.OldPassword, user.Salt))
            {
                throw new AuthenticationException();
            }
            user.EncodedPassword = HashUtils.HashPassword(dto.Password, user.Salt);
            _userRepository.Update(user);
        }

        public bool ExistsEmail(string email, Guid? id)
        {
            return _userRepository.Any(u => u.Email.ToLower() == email.ToLower() && u.Id != id);
        }

        public bool ExistsUsername(string username, Guid? id)
        {
            return _userRepository.Any(u => u.Username.ToLower() == username.ToLower() && u.Id != id);
        }
    }
}
