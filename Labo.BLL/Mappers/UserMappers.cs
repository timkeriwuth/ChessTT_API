using Labo.BLL.DTO.Users;
using Labo.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.BLL.Mappers
{
    internal static class UserMappers
    {
        public static User ToEntity(this MemberFormDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
            };
        }
    }
}
