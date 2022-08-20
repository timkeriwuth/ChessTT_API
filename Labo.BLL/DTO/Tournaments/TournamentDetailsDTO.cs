using Labo.BLL.DTO.Matches;
using Labo.BLL.DTO.Users;
using Labo.DL.Entities;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentDetailsDTO : TournamentDTO
    {
        public IEnumerable<UserDTO> Players { get; set; }
        public bool CanStart { get; set; }
        public bool CanValidateRound { get; set; }
        public IEnumerable<MatchDTO> Matches { get; set; }
        public TournamentDetailsDTO(Tournament tournament) : base(tournament)
        {
            Players = tournament.Players.Select(p => new UserDTO(p));
            Matches = tournament.Matches.Select(p => new MatchDTO(p));
        }
    }
}
