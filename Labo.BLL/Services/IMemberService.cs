using Labo.BLL.DTO.Users;

namespace Labo.BLL.Services
{
    public interface IMemberService
    {
        void Add(MemberFormDTO dto);
        void ChangePassword(Guid id, ChangePasswordDTO dto);
        bool ExistsEmail(ExistsEmailDTO dto);
        bool ExistsUsername(ExistsUsernameDTO dto);
    }
}