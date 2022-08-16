using Labo.DL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Produces(typeof(IEnumerable<string>))]
        public IActionResult Get()
        {
            return Ok(Enum.GetNames(typeof(TournamentCategory)));
        }
    }
}
