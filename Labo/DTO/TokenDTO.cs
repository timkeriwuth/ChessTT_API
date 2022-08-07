using Labo.BLL.DTO.Users;

namespace Labo.API.DTO
{
    public class TokenDTO
    {
        public TokenDTO(string token, UserDTO user)
        {
            Token = token;
            User = user;
        }

        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
}
