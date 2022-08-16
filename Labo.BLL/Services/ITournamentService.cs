using Labo.BLL.DTO.Tournaments;

namespace Labo.BLL.Services
{
    public interface ITournamentService
    {
        int Count(TournamentCriteriaDTO criteria);
        IEnumerable<TournamentDTO> Find(TournamentCriteriaDTO criteria, Guid userId);
        Guid Add(TournamentAddDTO dto);
        Guid Remove(Guid id);
        void Register(Guid userId, Guid tournamentId);
        void Unregister(Guid userId, Guid tournamentId);
        TournamentDetailsDTO GetWithPlayers(Guid id, int? round, Guid userId);
        void Start(Guid id);
        void ValidateRound(Guid id);
    }
}