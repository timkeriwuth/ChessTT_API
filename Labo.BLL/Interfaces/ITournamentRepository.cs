using Labo.DL.Entities;
using Labo.DL.Enums;
using ToolBox.EF.Repository;

namespace Labo.BLL.Interfaces
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        int CountByCriteria(string? name, TournamentCategory? category, IEnumerable<TournamentStatus>? statuses, bool wonenOnly = false);
        IEnumerable<Tournament> FindWithPlayersByCriteriaOrderByCreationDateDesc(string? name, TournamentCategory? category, IEnumerable<TournamentStatus>? statuses, bool wonenOnly = false, int offset = 0, int limit = 10);
        Tournament? FindOneWithPlayers(Guid tournamentId);
        void AddPlayer(Tournament tournament, User user);
        void RemovePlayer(Tournament tournament, User user);
        Tournament? FindOneWithPlayersAndMatches(Guid tournamentId);
    }
}