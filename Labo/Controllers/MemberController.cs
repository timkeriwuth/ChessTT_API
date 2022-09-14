using Labo.API.DTO;
using Labo.API.Extensions;
using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
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
        public async Task<IActionResult> PostAsync([FromBody] MemberFormDTO dto)
        {
            try
            {
                await _memberService.AddAsync(dto);
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

        [HttpHead("existsEmail")]
        public IActionResult ExistsEmail([FromQuery] ExistsEmailDTO dto)
        {
            try
            {
                if (_memberService.ExistsEmail(dto))
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

        [HttpHead("existsUsername")]
        public IActionResult ExistUsername([FromQuery] ExistsUsernameDTO dto)
        {
            try
            {
                if (_memberService.ExistsUsername(dto))
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
