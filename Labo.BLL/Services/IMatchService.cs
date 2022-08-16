using Labo.BLL.DTO.Matches;

namespace Labo.BLL.Services
{
    public interface IMatchService
    {
        IEnumerable<MatchDTO> Get(Guid tournamentId);
    }
}