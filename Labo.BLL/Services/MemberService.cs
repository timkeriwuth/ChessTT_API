using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
using Labo.BLL.Mappers;
using Labo.DL.Entities;
using Labo.DL.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Transactions;
using ToolBox.Security.Utils;

namespace Labo.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMailer _mailer;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordGenerator _passwordGenerator;

        private readonly string RegisterMailTemplate = @"
            <h1>Your registration for Mr Checkmate's plateform</h1>
            <div>
                <p>Your Credentials:</p>
                <p>username: __username__</p>
                <p>password: __password__</p>
            </div>
        ";

        public MemberService(IMailer mailer, IUserRepository userRepository, IPasswordGenerator passwordGenerator)
        {
            _mailer = mailer;
            _userRepository = userRepository;
            _passwordGenerator = passwordGenerator;
        }

        public async Task AddAsync(MemberFormDTO dto)
        {
            if (_userRepository.Any(u => u.Username.ToLower() == dto.Username.ToLower()))
            {
                throw new ValidationException("This username already exists") { Source = nameof(dto.Username) };
            }
            if (_userRepository.Any(u => u.Email.ToLower() == dto.Email.ToLower()))
            {
                throw new ValidationException("This email already exists") { Source = nameof(dto.Email) };
            }
            string password = _passwordGenerator.Random(8);
            User u = dto.ToEntity();
            u.Elo = dto.Elo ?? 1200;
            u.Role = UserRole.Player;
            u.Salt = Guid.NewGuid();
            u.EncodedPassword = HashUtils.HashPassword(password, u.Salt);
            u.IsDeleted = false;


            using TransactionScope t = new(TransactionScopeAsyncFlowOption.Enabled);
            {
                _userRepository.Add(u);
                await _mailer.SendAsync(
                    "New Registration",
                    RegisterMailTemplate
                        .Replace("__username__", u.Username)
                        .Replace("__password__", password),
                    u.Email // replaced in debug
                );
            }
            t.Complete();
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

        public bool ExistsEmail(ExistsEmailDTO dto)
        {
            return _userRepository.Any(u => u.Email.ToLower() == dto.Email.ToLower() && u.Id != dto.ExcludeId);
        }

        public bool ExistsUsername(ExistsUsernameDTO dto)
        {
            return _userRepository.Any(u => u.Username.ToLower() == dto.Username.ToLower() && u.Id != dto.ExcludeId);
        }
    }
}
