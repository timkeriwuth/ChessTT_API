using Labo.BLL.Validators;
using Labo.DL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Labo.BLL.DTO.Users
{
    public class MemberFormDTO
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BeforeToday]
        [Required]
        public DateTime BirthDate { get; set; }

        [Range(0,3000)]
        public int? Elo { get; set; }

        [Required]
        public UserGender Gender { get; set; }
    }
}
