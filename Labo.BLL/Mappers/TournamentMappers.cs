using Labo.BLL.DTO.Tournaments;
using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.Mappers
{
    internal static class TournamentMappers
    {
        public static Tournament ToEntity(this TournamentAddDTO dto)
        {
            return new Tournament
            {
                Name = dto.Name,
                Location = dto.Location,
                EloMin = dto.EloMin,
                EloMax = dto.EloMax,
                MinPlayers = dto.MinPlayers,
                MaxPlayers = dto.MaxPlayers,
                WomenOnly = dto.WomenOnly,
                EndOfRegistrationDate = dto.EndOfRegistrationDate,
                Categories = (TournamentCategory)dto.Categories.Cast<int>().Sum(),
            };
        }
    }
}
