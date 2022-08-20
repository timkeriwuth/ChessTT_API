using Labo.DL.Enums;
using System.ComponentModel.DataAnnotations;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentSearchDTO
    {
        public int Offset { get; set; } = 0;
        public string? Name { get; set; }
        public TournamentCategory? Category { get; set; }
        public IEnumerable<TournamentStatus>? Statuses { get; set; } = new List<TournamentStatus>() { TournamentStatus.WaitingForPlayers, TournamentStatus.InProgress };
        public bool WomenOnly { get; set; }
    }
}
