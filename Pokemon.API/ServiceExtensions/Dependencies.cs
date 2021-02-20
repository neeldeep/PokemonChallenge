using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pokemon.Services;
using Pokemon.Services.Interfaces;

namespace Pokemon.API.ServiceExtensions
{
    internal static class Dependencies
    {
        public static void Add(IServiceCollection services)
        {
            services.TryAddSingleton<IPokeAPIClient, PokeAPIClientService>();
            services.TryAddSingleton<IPokemonService, PokemonService>();
            services.AddTransient<ITranslateService, TranslateService>();
        }
    }
}
