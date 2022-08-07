using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.Exceptions
{
    public class TournamentRegistrationException: Exception
    {
        public TournamentRegistrationException(string message)
            : base(message) { }
    }
}
