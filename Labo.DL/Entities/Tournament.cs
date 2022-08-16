using Labo.DL.Enums;

namespace Labo.DL.Entities
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? EloMin { get; set; }
        public int? EloMax { get; set; }
        public TournamentCategory Categories { get; set; }
        public TournamentStatus Status { get; set; }
        public bool WomenOnly { get; set; }
        public int CurrentRound { get; set; }
        public DateTime EndOfRegistrationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<User> Players { get; set; } = new List<User>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
