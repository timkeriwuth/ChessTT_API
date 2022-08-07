using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.Exceptions
{
    public class TournamentException: Exception
    {
        public TournamentException(string message)
            : base(message) { }
    }
}
