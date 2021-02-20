using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Pokemon.Entities;
using Pokemon.Services;

namespace Pokemon.Tests.Services
{
    public class PokemanClientAPITest
    {
        private Mock<ILogger<PokeAPIClientService>> logger;
        
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<PokeAPIClientService>>();
        }
        [Test]
        public void TestInvalidPokemonNameException()
        {
            // Given an invalid Pokemon Name
            var invaidPokemonName = "thereisnosuchpokemon";
            // When calling the Pokemon API
            var pokeclientService = new PokeAPIClientService(logger.Object);

            // Then Exception is thrown
            Assert.ThrowsAsync<APIException>(() => pokeclientService.GetPokemonByName(invaidPokemonName));
        }

        [Test]
        public void TestValidPokemonNoException()
        {
            // Given an valid Pokemon Name
            var validPokemonName = "pikachu";
            // When calling the Pokemon API
            var pokeclientService = new PokeAPIClientService(logger.Object);

            // Then No Exception is Thrown & Data is returned
            Assert.DoesNotThrowAsync(() => pokeclientService.GetPokemonByName(validPokemonName));
            Assert.IsNotNull(pokeclientService.GetPokemonByName(validPokemonName));
        }
    }
}
