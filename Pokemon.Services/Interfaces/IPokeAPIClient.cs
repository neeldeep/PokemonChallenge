using PokeApiNet;
using System.Threading.Tasks;

namespace Pokemon.Services.Interfaces
{
    public interface IPokeAPIClient
    {
        Task<PokemonSpecies> GetPokemonByName(string pokemonName);
    }
}
