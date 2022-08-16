using Labo.BLL.Validators;
using Labo.DL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.DTO.Tournaments
{
    public class TournamentAddDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Location { get; set; }

        [Required]
        [Range(2, 16)]
        public int MinPlayers { get; set; }

        [Required]
        [Range(2, 16)]
        [GreaterOrEqualThan(nameof(MinPlayers))]
        public int MaxPlayers { get; set; }

        [Range(0, 3000)]
        public int? EloMin { get; set; }

        [Range(0, 3000)]
        [GreaterOrEqualThan(nameof(EloMin))]
        public int? EloMax { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<TournamentCategory> Categories { get; set; } = Enumerable.Empty<TournamentCategory>();

        [Required]
        public bool WomenOnly { get; set; }

        [Required]
        public DateTime EndOfRegistrationDate { get; set; }
    }
}
