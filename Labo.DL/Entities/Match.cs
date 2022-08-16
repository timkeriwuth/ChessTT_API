using Labo.DL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.DL.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public Guid TournamentId { get; set; }
        public Guid WhiteId { get; set; }
        public Guid BlackId { get; set; }
        public int Round { get; set; }
        public MatchResult Result { get; set; }
        public virtual Tournament Tournament { get; set; } = null!;
        public virtual User White { get; set; } = null!;
        public virtual User Black { get; set; } = null!;
    }
}
