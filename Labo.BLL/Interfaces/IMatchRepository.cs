using Labo.DL.Entities;
using ToolBox.EF.Repository;

namespace Labo.BLL.Interfaces
{
    public interface IMatchRepository : IRepository<Match>
    {
        IEnumerable<Match> FindWithPlayersByTournamentAndRound(Guid tournamentId, int round);
        Match? FindOneWithTournament(int id);
    }
}