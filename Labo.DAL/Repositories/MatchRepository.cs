using Labo.BLL.Interfaces;
using Labo.DAL.Contexts;
using Labo.DL.Entities;
using Microsoft.EntityFrameworkCore;
using ToolBox.EF.Repository;

namespace Labo.DAL.Repositories
{
    public class MatchRepository : RepositoryBase<Match>, IMatchRepository
    {
        public MatchRepository(TournamentContext context) : base(context)
        {
        }

        public IEnumerable<Match> FindWithPlayersByTournamentAndRound(Guid tournamentId, int round)
        {
            return _entities
                .Include(m => m.White)
                .Include(m => m.Black)
                .Where(m => m.TournamentId == tournamentId && m.Round == round);
        }

        public Match? FindOneWithTournament(int id)
        {
            return _entities.Include(m => m.Tournament).FirstOrDefault(m => m.Id == id);
        }
    }
}
