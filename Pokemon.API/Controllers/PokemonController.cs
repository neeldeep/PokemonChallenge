using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokemon.Entities;
using Pokemon.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace Pokemon.API.Controllers
{
    [Route("pokemon/")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonService _pokemonSvc;
        
        /// <summary>
        /// Pokemon Service and Logger is Injected in the Controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="pokemonSvc"></param>
        public PokemonController(
          ILogger<PokemonController> logger,
          IPokemonService pokemonSvc
      )
        {
            _logger = logger;
            _pokemonSvc = pokemonSvc;
        }



        /// <summary>
        ///  Retrieves the Shakespearian description of a pokemon name
        /// </summary>
        /// <param name="name">Request containing pokemon name</param>
        /// <returns>Returns JSON containing Pokemon Name and Description</returns>
        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PokemonResponse), StatusCodes.Status200OK)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, Description = "Returns Shakespeare Description of Pokemon Name")]
        [Route("/pokemon/{name}")]
        public async Task<IActionResult> GetPokemonDescByName(string name)
        {
                if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("No Pokemon Name Provided");
                throw new APIException(HttpStatusCode.BadRequest, "Please provide a Pokemon Name");
            }

            var pokemonresp = await _pokemonSvc.GetPokemonDescByName(name);
            return Ok(pokemonresp);
        }
    }
}

