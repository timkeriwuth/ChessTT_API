using Labo.BLL.DTO.Users;

namespace Labo.BLL.Services
{
    public interface IMemberService
    {
        void Add(MemberFormDTO dto);
        void ChangePassword(Guid id, ChangePasswordDTO dto);
        bool ExistsEmail(string email, Guid? id);
        bool ExistsUsername(string username, Guid? id);
    }
}