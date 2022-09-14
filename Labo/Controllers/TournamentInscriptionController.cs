using Labo.API.Extensions;
using Labo.BLL.Exceptions;
using Labo.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentInscriptionController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentInscriptionController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpPost("{id}")]
        [Authorize]
        public IActionResult Post(Guid id)
        {
            try
            {
                _tournamentService.Register(User.GetId(), id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (TournamentRegistrationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _tournamentService.Unregister(User.GetId(), id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (TournamentRegistrationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
