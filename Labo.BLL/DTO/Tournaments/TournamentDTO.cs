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
        public TournamentCategory Categories { get; set; }
        public bool WomenOnly { get; set; }
        public DateTime EndOfRegistrationDate { get; set; }

        public TournamentDTO(Tournament tournament)
        {
            Id = tournament.Id;
            Name = tournament.Name;
            MinPlayers = tournament.MinPlayers;
            MaxPlayers = tournament.MaxPlayers;
            EloMin = tournament.EloMin;
            EloMax = tournament.EloMax;
            Categories = tournament.Categories;
            EndOfRegistrationDate = tournament.EndOfRegistrationDate;
            Location = tournament.Location;
            WomenOnly = tournament.WomenOnly;
        }
    }
}
