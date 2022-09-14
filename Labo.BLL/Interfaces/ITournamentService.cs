using Labo.BLL.DTO.Tournaments;

namespace Labo.BLL.Interfaces
{
    public interface ITournamentService
    {
        int Count(TournamentSearchDTO criteria);
        IEnumerable<TournamentDTO> Find(TournamentSearchDTO criteria, Guid userId);
        Guid Add(TournamentAddDTO dto);
        Guid Remove(Guid id);
        void Register(Guid userId, Guid tournamentId);
        void Unregister(Guid userId, Guid tournamentId);
        TournamentDetailsDTO GetWithPlayers(Guid id, Guid userId);
        void Start(Guid id);
        void ValidateRound(Guid id);
    }
}