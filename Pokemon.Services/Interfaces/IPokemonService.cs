using Pokemon.Entities;
using System.Threading.Tasks;

namespace Pokemon.Services.Interfaces
{
   public interface IPokemonService
    {
        Task<PokemonResponse> GetPokemonDescByName(string pokemonName);
    }
}
