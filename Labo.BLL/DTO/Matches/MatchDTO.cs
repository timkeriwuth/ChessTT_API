using Labo.BLL.DTO.Users;
using Labo.DL.Entities;
using Labo.DL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.DTO.Matches
{
    public class MatchDTO
    {
        public MatchDTO(Match m)
        {
            Id = m.Id;
            TournamentId = m.TournamentId;
            BlackId = m.BlackId;
            WhiteId = m.WhiteId;
            Result = m.Result;
            WhiteName = m.White.Username;
            BlackName = m.Black.Username;
            Round = m.Round;
        }

        public int Id { get; set; }
        public Guid TournamentId { get; set; }
        public string BlackName { get; set; }
        public Guid BlackId { get; set; }
        public string WhiteName { get; set; }
        public Guid WhiteId { get; set; }
        public MatchResult Result { get; set; }
        public int Round { get; set; }
    }
}
