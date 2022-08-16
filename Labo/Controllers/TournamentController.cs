using Labo.API.DTO;
using Labo.API.Extensions;
using Labo.BLL.DTO.Tournaments;
using Labo.BLL.Exceptions;
using Labo.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<TournamentDTO>))]
        public IActionResult Get([FromQuery] TournamentCriteriaDTO criteria)
        {
            try
            {
                Response.AddTotalHeader(_tournamentService.Count(criteria));
                return Ok(_tournamentService.Find(criteria, User.GetId()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id})")]
        public IActionResult Get(Guid id, [FromQuery]int? round)
        {
            try
            {
                TournamentDetailsDTO dto = _tournamentService.GetWithPlayers(id, round, User.GetId());
                return Ok(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Produces(typeof(Guid))]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] TournamentAddDTO dto)
        {
            try
            {
                Guid id = _tournamentService.Add(dto);
                return Ok(id);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationErrorDTO(ex));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Guid))]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _tournamentService.Remove(id);
                return Ok(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (TournamentException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPatch("{id}/start")]
        [Authorize(Roles = "Admin")]
        public IActionResult Start(Guid id)
        {
            try
            {
                _tournamentService.Start(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (TournamentException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPatch("{id}/nextRound")]
        [Authorize(Roles = "Admin")]
        public IActionResult ValidateRound(Guid id)
        {
            try
            {
                _tournamentService.ValidateRound(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (TournamentException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
