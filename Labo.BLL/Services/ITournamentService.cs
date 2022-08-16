using Labo.BLL.DTO.Tournaments;

namespace Labo.BLL.Services
{
    public interface ITournamentService
    {
        int Count(TournamentCriteriaDTO criteria);
        IEnumerable<TournamentDTO> Find(TournamentCriteriaDTO criteria, Guid id);
        Guid Add(TournamentAddDTO dto);
        Guid Remove(Guid id);
        void Register(Guid userId, Guid tournamentId);
        void Unregister(Guid userId, Guid tournamentId);
        TournamentDetailsDTO GetWithPlayers(Guid id);
        void Start(Guid id);
    }
}