using Labo.API.DTO;
using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Labo.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthenticationService authenticationService, IJwtService jwtService)
        {
            _authenticationService = authenticationService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [Produces(typeof(TokenDTO))]
        public IActionResult Login(LoginDTO dto)
        {
            try
            {
                UserDTO connectedUser = _authenticationService.Login(dto);
                string token = _jwtService.CreateToken(connectedUser.Id.ToString(), connectedUser.Email, connectedUser.Role.ToString());
                return Ok(new TokenDTO(token, connectedUser));
            }
            catch (AuthenticationException)
            {
                return BadRequest("Bad credentials");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
