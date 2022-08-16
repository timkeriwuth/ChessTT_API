using Labo.API.DTO;
using Labo.API.Extensions;
using Labo.BLL.DTO.Users;
using Labo.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Authentication;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] MemberFormDTO dto)
        {
            try
            {
                _memberService.Add(dto);
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

        [HttpPatch("password")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                _memberService.ChangePassword(User.GetId(), dto);
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

        [HttpHead("canUseEmail")]
        public IActionResult CanUseEmail([FromQuery][EmailAddress] string email, [FromQuery] Guid? id)
        {
            try
            {
                if (_memberService.ExistsEmail(email, id))
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpHead("canUseUsername")]
        public IActionResult CanUseUsername([FromQuery] string username, [FromQuery] Guid? id)
        {
            try
            {
                if (_memberService.ExistsUsername(username, id))
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
