using Labo.BLL.DTO.Matches;
using Labo.BLL.DTO.Users;
using Labo.DAL.Repositories;
using Labo.DL.Entities;

namespace Labo.BLL.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public IEnumerable<MatchDTO> Get(Guid tournamentId)
        {
            throw new NotImplementedException();
        }

    }
}
