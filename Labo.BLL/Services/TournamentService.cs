using Labo.BLL.DTO.Tournaments;
using Labo.BLL.Mappers;
using System.ComponentModel.DataAnnotations;
using Labo.DL.Entities;
using Labo.DL.Enums;
using Labo.BLL.Exceptions;
using Labo.BLL.Interfaces;

namespace Labo.BLL.Services
{
    public class TournamentService: ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailer _mailer;

        public TournamentService(ITournamentRepository tournamentRepository, IUserRepository userRepository, IMailer mailer)
        {
            _tournamentRepository = tournamentRepository;
            _userRepository = userRepository;
            _mailer = mailer;
        }

        public IEnumerable<TournamentDTO> Find(TournamentSearchDTO criteria, Guid userId)
        {
            User? u = _userRepository.FindOne(userId);

            return _tournamentRepository.FindWithPlayersByCriteriaOrderByCreationDateDesc(
                criteria.Name,
                criteria.Category,
                criteria.Statuses,
                criteria.WomenOnly,
                criteria.Offset
            ).Select(t =>
            {
                TournamentDTO dto = new(t);
                if (u is not null)
                {
                    dto.IsRegistered = t.Players.Contains(u);
                    dto.CanRegister = CanRegister(u, t);
                }
                return dto;
            });
        }

        public int Count(TournamentSearchDTO criteria)
        {
            return _tournamentRepository.CountByCriteria(
                criteria.Name, 
                criteria.Category,
                criteria.Statuses,
                criteria.WomenOnly
            );
        }

        public TournamentDetailsDTO GetWithPlayers(Guid tournamentId, Guid userId)
        {
            User? u = _userRepository.FindOne(userId);
            Tournament? tournament = _tournamentRepository.FindOneWithPlayersAndMatches(tournamentId);
            if (tournament is null)
            {
                throw new KeyNotFoundException();
            }
            TournamentDetailsDTO dto = new(tournament);
            dto.CanStart = CanStart(tournament);
            dto.CanValidateRound = CanValidateRound(tournament);
            if (u is not null)
            {
                dto.IsRegistered = tournament.Players.Contains(u);
                dto.CanRegister = CanRegister(u, tournament);
            }
            return dto;
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
            Tournament newTournament = _tournamentRepository.Add(t);
            IEnumerable<User> canParticipatePlayers = _userRepository
                .Find(u => CanRegister(u, newTournament));

            try
            {
                _mailer.SendAsync(
                    "New Tournament",
                    $"<p>A new tournmaent {t.Name} is created</p>",
                    canParticipatePlayers.Select(p => p.Email).ToArray()
                );
            }
            catch (Exception) { }

            return t.Id;
        }

        public Guid Remove(Guid id)
        {
            Tournament? t = _tournamentRepository.FindOneWithPlayers(id);
            if (t is null)
            {
                throw new KeyNotFoundException();
            }
            if(t.Status != TournamentStatus.WaitingForPlayers)
            {
                throw new TournamentException("Cannot remove a tournament that has already started");
            }
            _tournamentRepository.Remove(t);

            try
            {
                _mailer.SendAsync(
                    "Tournament canceled",
                    $"<p>The tournament {t.Name} has been canceled</p>",
                    t.Players.Select(p => p.Email).ToArray()
                );
            }
            catch (Exception) { }


            return id;
        }

        public void Start(Guid id)
        {
            Tournament? t = _tournamentRepository.FindOneWithPlayers(id);
            if (t is null)
            {
                throw new KeyNotFoundException();
            }
            CheckStart(t);
            t.Status = TournamentStatus.InProgress;
            t.CurrentRound = 1;
            GenerateMatches(t);
            _tournamentRepository.Update(t);
        }

        public void ValidateRound(Guid id)
        {
            Tournament? t = _tournamentRepository.FindOneWithPlayersAndMatches(id);
            if (t is null)
            {
                throw new KeyNotFoundException();
            }
            CheckValidateRound(t);
            if (t.CurrentRound == (t.Players.Count() - 1) * 2)
            {
                t.Status = TournamentStatus.Closed;
            }
            else
            {
                t.CurrentRound++;
            }
            _tournamentRepository.Update(t);

        }

        public void Register(Guid userId, Guid tournamentId)
        {
            User? player = _userRepository.FindOne(userId);
            Tournament? tournament = _tournamentRepository.FindOneWithPlayers(tournamentId);
            if (tournament == null)
            {
                throw new KeyNotFoundException();
            }
            if (player == null)
            {
                throw new UnauthorizedAccessException();
            }
            CheckCanRegister(player, tournament);
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
            if (player == null)
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

        private static bool CanRegister(User player, Tournament tournament)
        {
            try
            {
                CheckCanRegister(player, tournament);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private static void CheckCanRegister(User player, Tournament tournament)
        {
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
        }

        private static bool CanValidateRound(Tournament t)
        {
            try
            {
                CheckValidateRound(t);
                return true;
            }
            catch (Exception) { return false; }
        }

        private static void CheckValidateRound(Tournament t)
        {
            if (t.Status != TournamentStatus.InProgress)
            {
                throw new TournamentException("Cannot validate a round when the tournament is not in progress");
            }
            if (t.Matches.Any(m => m.Round <= t.CurrentRound && m.Result == MatchResult.NotPlayed))
            {
                throw new TournamentException("Cannot validate a round when the all the matches are not played");
            }
        }

        private static bool CanStart(Tournament t)
        {
            try
            {
                CheckStart(t);
                return true;
            }
            catch (Exception) { return false; }
        }

        private static void CheckStart(Tournament t)
        {
            if (t.Status != TournamentStatus.WaitingForPlayers)
            {
                throw new TournamentException("Cannot start a tournament that has already started");
            }
            if (t.Players.Count < t.MinPlayers)
            {
                throw new TournamentException("Not enough players");
            }
            // TODO remove comment
            // temporary removed for testing
            //if (t.EndOfRegistrationDate > DateTime.Now)
            //{
            //    throw new TournamentException("Cannot start a tournament before the end of registration date");
            //}
        }

        private static void CheckCategories(Tournament tournament, User player)
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

        private static int CalculateAge(Tournament tournament, User player)
        {
            int age = tournament.EndOfRegistrationDate.Year - player.BirthDate.Year;

            if (player.BirthDate > tournament.EndOfRegistrationDate.AddYears(-age)) age--;

            return age;
        }

        private static void CheckElo(Tournament tournament, User player)
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

        private static void GenerateMatches(Tournament t)
        {
            List<Guid> players = t.Players.Select(p => p.Id).ToList();
            if(players.Count % 2 == 1)
            {
                players.Add(default);
            }
            for(int round = 1; round < players.Count * 2 - 1; round++) 
            {
                for(int i = 0; i < players.Count / 2; i++)
                {
                    if(players[i] != default && players[players.Count - 1 - i] != default)
                    {
                        t.Matches.Add(new Match
                        {
                            TournamentId = t.Id,
                            WhiteId = round % 2 == 1 ? players[i] : players[players.Count - 1 - i],
                            BlackId = round % 2 == 1 ? players[players.Count - 1 - i] : players[i],
                            Round = round,
                            Result = MatchResult.NotPlayed,
                        });
                    }
                }
                InsertLastItemToSecondPlace(players);
            }

        }

        private static void InsertLastItemToSecondPlace(List<Guid> players)
        {
            players.Insert(1, players.Last());
            players.RemoveAt(players.Count - 1);
        }
    }
}
