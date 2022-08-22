using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentIndexDTO
    {
        public TournamentIndexDTO(int total, IEnumerable<TournamentDTO> results)
        {
            Total = total;
            Results = results;
        }

        public int Total { get; set; }
        public IEnumerable<TournamentDTO> Results { get; set; }
    }
}
