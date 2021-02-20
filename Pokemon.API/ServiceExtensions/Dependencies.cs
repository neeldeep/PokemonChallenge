using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pokemon.Services;
using Pokemon.Services.Interfaces;

namespace Pokemon.API.ServiceExtensions
{
    internal static class Dependencies
    {
        /// <summary>
        /// This class is created to declare the Dependences in a single place. Also separating out to keep the Startup.cs clean
        /// </summary>
        /// <param name="services"></param>
        public static void Add(IServiceCollection services)
        {
            services.TryAddSingleton<IPokeAPIClient, PokeAPIClientService>();
            services.TryAddSingleton<IPokemonService, PokemonService>();
            services.AddTransient<ITranslateService, TranslateService>();
        }
    }
}
