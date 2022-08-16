using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? EloMin { get; set; }
        public int? EloMax { get; set; }
        public IEnumerable<TournamentCategory> Categories { get; set; }
        public bool WomenOnly { get; set; }
        public DateTime EndOfRegistrationDate { get; set; }
        public int Count { get; set; }
        public bool CanRegister { get; set; }
        public bool IsRegistered { get; set; }
        public TournamentStatus Status { get; set; }
        public int CurrentRound { get; set; }

        public TournamentDTO(Tournament tournament)
        {
            Id = tournament.Id;
            Name = tournament.Name;
            MinPlayers = tournament.MinPlayers;
            MaxPlayers = tournament.MaxPlayers;
            EloMin = tournament.EloMin;
            EloMax = tournament.EloMax;
            Categories = Enum.GetValues<TournamentCategory>().Where(v => tournament.Categories.HasFlag(v));
            EndOfRegistrationDate = tournament.EndOfRegistrationDate;
            Location = tournament.Location;
            WomenOnly = tournament.WomenOnly;
            Status = tournament.Status;
            CurrentRound = tournament.CurrentRound;
            Count = tournament.Players.Count;
        }
    }
}
