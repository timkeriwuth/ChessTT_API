using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.DTO.Users
{
    public class PlayerScoreDTO
    {
        public PlayerScoreDTO(User p)
        {
            Id = p.Id;
            Username = p.Username;
            MatchPlayed = p.MatchesAsWhite.Count() + p.MatchesAsBlack.Count();
            Wins = p.MatchesAsWhite.Count(m => m.Result == MatchResult.WhiteWin) + p.MatchesAsBlack.Count(m => m.Result == MatchResult.BlackWin);
            Defeats = p.MatchesAsWhite.Count(m => m.Result == MatchResult.BlackWin) + p.MatchesAsBlack.Count(m => m.Result == MatchResult.WhiteWin);
            Draws = p.MatchesAsWhite.Count(m => m.Result == MatchResult.Draw) + p.MatchesAsBlack.Count(m => m.Result == MatchResult.Draw);
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int MatchPlayed { get; set; }
        public int Wins { get; set; }
        public int Defeats { get; set; }
        public int Draws { get; set; }
        public double Score => Wins * 1 + Draws * 0.5;
    }
}
