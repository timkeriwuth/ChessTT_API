using Labo.BLL.DTO.Users;

namespace Labo.BLL.Services
{
    public interface IMemberService
    {
        Task AddAsync(MemberFormDTO dto);
        void ChangePassword(Guid id, ChangePasswordDTO dto);
        bool ExistsEmail(ExistsEmailDTO dto);
        bool ExistsUsername(ExistsUsernameDTO dto);
    }
}