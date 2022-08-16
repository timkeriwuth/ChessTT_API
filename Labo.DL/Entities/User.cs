using Labo.DL.Enums;

namespace Labo.DL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] EncodedPassword { get; set; } = Array.Empty<byte>();
        public Guid Salt { get; set; }
        public DateTime BirthDate { get; set; }
        public int Elo { get; set; }
        public UserGender Gender { get; set; }
        public UserRole Role { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public ICollection<Match> MatchesAsWhite { get; set; } = new List<Match>();
        public ICollection<Match> MatchesAsBlack { get; set; } = new List<Match>();

    }
}
