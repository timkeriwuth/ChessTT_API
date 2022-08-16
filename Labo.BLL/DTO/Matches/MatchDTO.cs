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
            White = new UserDTO(m.White);
            Black = new UserDTO(m.Black);
        }

        public int Id { get; set; }
        public Guid TournamentId { get; set; }
        public UserDTO Black { get; set; }
        public Guid BlackId { get; set; }
        public UserDTO White { get; set; }
        public Guid WhiteId { get; set; }
        public MatchResult Result { get; set; }
    }
}
