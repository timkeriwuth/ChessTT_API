using Labo.BLL.DTO.Tournaments;

namespace Labo.BLL.Services
{
    public interface ITournamentService
    {
        int Count(TournamentCriteriaDTO criteria);
        IEnumerable<TournamentDTO> Find(TournamentCriteriaDTO criteria);
        Guid Add(TournamentAddDTO dto);
        Guid Remove(Guid id);
        void Register(Guid guid, Guid id);
        void Unregister(Guid guid, Guid id);
        TournamentDetailsDTO GetWithPlayers(Guid id);
    }
}