using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.DTO.Users
{
    public class ExistsUsernameDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        public Guid? ExcludeId { get; set; }
    }
}
