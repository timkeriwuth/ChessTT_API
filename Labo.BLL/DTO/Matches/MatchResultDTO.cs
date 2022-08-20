using Labo.DL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.DTO.Matches
{
    public class MatchResultDTO
    {
        [Required]
        public MatchResult Result { get; set; }
    }
}
