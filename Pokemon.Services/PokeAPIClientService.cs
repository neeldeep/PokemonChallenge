using Microsoft.Extensions.Logging;
using PokeApiNet;
using Pokemon.Entities;
using Pokemon.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pokemon.Services
{
    // We have created this call to encapsulate Poke API.Net Client
    public class PokeAPIClientService : IPokeAPIClient
    {
        PokeApiClient pokeApiClientObj;
        ILogger<PokeAPIClientService> _logger;
       public PokeAPIClientService(ILogger<PokeAPIClientService> logger)
        {
            _logger = logger;
            if (pokeApiClientObj == null)
            {
                pokeApiClientObj = new PokeApiClient();
            }
        }
        /// <summary>
        /// Get Pokemon By Name using Poki API .Net Client
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns>Task<PokemonSpecies></returns>
        public async Task<PokemonSpecies> GetPokemonByName(string pokemonName)
        {
            try
            {
                return await pokeApiClientObj.GetResourceAsync
                        <PokemonSpecies>(pokemonName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new APIException(System.Net.HttpStatusCode.NotFound, "Invalid Pokemon Name");
            }
        }
    }
}
