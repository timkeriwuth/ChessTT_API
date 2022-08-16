using Labo.API.DTO;
using Labo.BLL.DTO.Users;
using Labo.BLL.Services;
using Labo.IL.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Labo.API.Extensions;
using Labo.IL.Utils;

namespace Labo.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtService _jwtService;
        private readonly IMailer _mailer;

        public AuthController(IAuthenticationService authenticationService, IJwtService jwtService, IMailer mailer)
        {
            _authenticationService = authenticationService;
            _jwtService = jwtService;
            _mailer = mailer;
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
            catch (SmtpFailedRecipientException)
            {
                return BadRequest("Invalid Email");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPatch("changePassword")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                _authenticationService.ChangePassword(User.GetId(), dto);
                return NoContent();
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

        [HttpGet("checkEmail")]
        [Produces(typeof(bool))]
        public IActionResult CheckEmail([FromQuery][EmailAddress] string email, [FromQuery]Guid? id)
        {
            try
            {
                return Ok(!_authenticationService.ExistsEmail(email, id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("checkUsername")]
        [Produces(typeof(bool))]
        public IActionResult CheckUsername([FromQuery] string username, [FromQuery] Guid? id)
        {
            try
            {
                return Ok(!_authenticationService.ExistsUsername(username, id));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
