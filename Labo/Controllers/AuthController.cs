using Labo.API.DTO;
using Labo.BLL.DTO.Users;
using Labo.BLL.Services;
using Labo.IL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        public IActionResult Login(LoginDTO dto)
        {
            try
            {
                UserDTO connectedUser = _authenticationService.Login(dto);
                string token = _jwtService.CreateToken(connectedUser.Id.ToString(), connectedUser.Role.ToString());
                return Ok(new TokenDTO(token, connectedUser));
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            try
            {
                _authenticationService.Register(dto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationErrorDTO(ex));
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
