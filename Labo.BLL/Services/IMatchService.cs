using Labo.BLL.DTO.Matches;
using Labo.DL.Enums;

namespace Labo.BLL.Services
{
    public interface IMatchService
    {
        IEnumerable<MatchDTO> Get(Guid tournamentId, int? round);
        void UpdateResult(int id, MatchResult result);
    }
}