using Microsoft.Extensions.Logging;
using Pokemon.Entities;
using Pokemon.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly ITranslateService _translateSvc;
        private readonly IPokeAPIClient _pokeClient;
        private readonly ILogger<PokemonService> _logger;

        
        /// <summary>
        /// Translate Service & Poke Client is Injected in Pokemon Service
        /// </summary>
        /// <param name="translateSvc"></param>
        public PokemonService(ILogger<PokemonService> logger,ITranslateService translateSvc, IPokeAPIClient pokeClient)
        {
            _translateSvc = translateSvc;
            _logger = logger;
            _pokeClient = pokeClient;
        }
        /// <summary>
        /// Get Pokemon Description By Name
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns>PokemonResponse</returns>
        public async Task<PokemonResponse> GetPokemonDescByName(string pokemonName)
        {
            var species = await _pokeClient.GetPokemonByName(pokemonName.ToLower());

            var description = species?.FlavorTextEntries?.FirstOrDefault(
                    c => string.Equals(c.Language.Name,"en", StringComparison.OrdinalIgnoreCase) && string.Equals(c.Version.Name,"ruby", StringComparison.OrdinalIgnoreCase));
            if (description == null || description.FlavorText == null)
            {
                _logger.LogError("No Description Available for Pokemon");
                throw new APIException(System.Net.HttpStatusCode.NotFound, "No Description Found");
            }
                var translatedTxt = await _translateSvc.GetShakespeareText(description.FlavorText);
                if(!string.IsNullOrEmpty(translatedTxt))
                    return new PokemonResponse() { PokemonName = pokemonName.ToLower(), ShakespeareDescription = translatedTxt };
                else
                {
                    _logger.LogError("Translation Error");
                    throw new APIException(System.Net.HttpStatusCode.NotFound, "Cannot Translate Text");
                }
        }

    }
}
