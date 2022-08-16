using Labo.BLL.DTO.Users;
using Labo.DL.Entities;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentDetailsDTO : TournamentDTO
    {
        public IEnumerable<PlayerScoreDTO> Players { get; set; }
        public TournamentDetailsDTO(Tournament tournament) : base(tournament)
        {
            Players = tournament.Players.Select(p => new PlayerScoreDTO(p));
        }
    }
}
