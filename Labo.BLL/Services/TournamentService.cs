using Labo.BLL.DTO.Tournaments;
using Labo.DAL.Repositories;
using Labo.BLL.Mappers;
using System.ComponentModel.DataAnnotations;
using Labo.DL.Entities;
using Labo.DL.Enums;
using Labo.BLL.Exceptions;

namespace Labo.BLL.Services
{
    public class TournamentService: ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IUserRepository _userRepository;

        public TournamentService(ITournamentRepository tournamentRepository, IUserRepository userRepository)
        {
            _tournamentRepository = tournamentRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<TournamentDTO> Find(TournamentCriteriaDTO criteria)
        {
            return _tournamentRepository.FindWithPlayersByCriteriaOrderByCreationDateDesc(
                criteria.Name,
                criteria.Category,
                criteria.Statuses,
                criteria.WomenOnly,
                criteria.Offset
            ).ToDTO();
        }

        public int Count(TournamentCriteriaDTO criteria)
        {
            return _tournamentRepository.CountByCriteria(
                criteria.Name, 
                criteria.Category,
                criteria.Statuses,
                criteria.WomenOnly
            );
        }

        public Guid Add(TournamentAddDTO dto)
        {
            if(dto.EndOfRegistrationDate < DateTime.Now.AddDays(dto.MinPlayers))
            {
                throw new ValidationException($"The End of Registration must be greater than { DateTime.Now.AddDays(dto.MinPlayers) }") { 
                    Source = nameof(dto.EndOfRegistrationDate)
                };
            }
            Tournament t = dto.ToEntity();
            t.Id = Guid.NewGuid();
            t.Status = TournamentStatus.WaitingForPlayers;
            t.CreationDate = DateTime.Now;
            t.UpdateDate = t.CreationDate;
            _tournamentRepository.Add(t);
            return t.Id;
        }

        public Guid Remove(Guid id)
        {
            Tournament? t = _tournamentRepository.FindOne(id);
            if (t is null)
            {
                throw new KeyNotFoundException();
            }
            if(t.Status != TournamentStatus.WaitingForPlayers)
            {
                throw new TournamentException("Cannot remove a tournament that has already started");
            }
            _tournamentRepository.Remove(t);
            return id;
        }

        public void Register(Guid userId, Guid tournamentId)
        {
            User? player = _userRepository.FindOne(userId);
            Tournament? tournament = _tournamentRepository.FindOneWithPlayers(tournamentId);
            if(tournament == null)
            {
                throw new KeyNotFoundException();
            }
            if (player == null || player.IsDeleted)
            {
                throw new UnauthorizedAccessException();
            }
            if (tournament.Status != TournamentStatus.WaitingForPlayers)
            {
                throw new TournamentRegistrationException("Cannot register when a tournament has already started");
            }
            if (tournament.Players.Contains(player))
            {
                throw new TournamentRegistrationException("This player is already registered");
            }
            if (tournament.WomenOnly && player.Gender == UserGender.Male)
            {
                throw new TournamentRegistrationException("Must be a women to participate");
            }
            if (tournament.Players.Count >= tournament.MaxPlayers)
            {
                throw new TournamentRegistrationException("This tournament is already full");
            }
            if (tournament.EndOfRegistrationDate < DateTime.Now)
            {
                throw new TournamentRegistrationException("The registration date has expired");
            }
            CheckElo(tournament, player);
            CheckCategories(tournament, player);
            _tournamentRepository.AddPlayer(tournament, player);
        }

        public void Unregister(Guid userId, Guid tournamentId)
        {
            User? player = _userRepository.FindOne(userId);
            Tournament? tournament = _tournamentRepository.FindOneWithPlayers(tournamentId);
            if (tournament == null)
            {
                throw new KeyNotFoundException();
            }
            if (player == null || player.IsDeleted)
            {
                throw new UnauthorizedAccessException();
            }
            if (tournament.Status != TournamentStatus.WaitingForPlayers)
            {
                throw new TournamentRegistrationException("Cannot unregister when a tournament has already started");
            }
            if (!tournament.Players.Contains(player))
            {
                throw new TournamentRegistrationException("This player is not in the tournament");
            }
            _tournamentRepository.RemovePlayer(tournament, player);
        }

        private void CheckCategories(Tournament tournament, User player)
        {
            bool flag = false;
            int age = CalculateAge(tournament, player);
            if(tournament.Categories.HasFlag(TournamentCategory.Junior) && age <= 18)
            {
                flag = true;
            }
            if (tournament.Categories.HasFlag(TournamentCategory.Senior) && age > 18 && age <= 60)
            {
                flag = true;
            }
            if (tournament.Categories.HasFlag(TournamentCategory.Veteran) && age > 60)
            {
                flag = true;
            }
            if (!flag)
            {
                throw new TournamentRegistrationException("This player doesn't have the required age");
            }
        }

        private int CalculateAge(Tournament tournament, User player)
        {
            int age = tournament.EndOfRegistrationDate.Year - player.BirthDate.Year;

            if (player.BirthDate > tournament.EndOfRegistrationDate.AddYears(-age)) age--;

            return age;
        }

        private void CheckElo(Tournament tournament, User player)
        {
            if(tournament.EloMin != null && player.Elo < tournament.EloMin)
            {
                throw new TournamentRegistrationException("This player doesn't have the required elo");
            }
            if (tournament.EloMax != null && player.Elo > tournament.EloMax)
            {
                throw new TournamentRegistrationException("This player doesn't have the required elo");
            }
        }

        public TournamentDetailsDTO GetWithPlayers(Guid id)
        {
            Tournament? tournament = _tournamentRepository.FindOneWithPlayers(id);
            if (tournament == null)
            {
                throw new KeyNotFoundException();
            }
            return new TournamentDetailsDTO(tournament);
        }
    }
}
