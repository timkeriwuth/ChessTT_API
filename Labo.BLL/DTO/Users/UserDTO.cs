using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.DTO.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Elo { get; set; }
        public UserGender Gender { get; set; }
        public UserRole Role { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            BirthDate = user.BirthDate;
            Elo = user.Elo;
            Gender = user.Gender;
            Role = user.Role;
        }

    }
}
