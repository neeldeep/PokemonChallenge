using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Pokemon.API.Controllers;
using Pokemon.Entities;
using Pokemon.Services.Interfaces;

namespace Pokemon.Tests.Controllers
{
   public class PokemonControllerTest
    {
        private Mock<ILogger<PokemonController>> logger;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<PokemonController>>();
        }

        [Test]
        public void TestNoPokemonNameException()
        {
            // Given an Blank Pokemon Name
            var pokemonNamewithblank = string.Empty;
            var pokesvc = new Mock<IPokemonService>();
            pokesvc.Setup(obj => obj.GetPokemonDescByName(It.IsAny<string>())).ReturnsAsync(It.IsAny<PokemonResponse>);

            // When Pokemon Controller is called
            var pokeCtlr = new PokemonController(logger.Object, pokesvc.Object);
            
            // API Exception is thrown
            Assert.ThrowsAsync<APIException>(() => pokeCtlr.GetPokemonDescByName(pokemonNamewithblank));

        }

        [Test]
        public void TestValidPokemonName()
        {
            // Given a Valid Pokemon name
            var pokesvc = new Mock<IPokemonService>();
            var pokeResp = new PokemonResponse() { PokemonName = "pikache", ShakespeareDescription = "test description" };
            pokesvc.Setup(obj => obj.GetPokemonDescByName(It.IsAny<string>())).ReturnsAsync(pokeResp);

            // When Pokemon Controller is called
            var pokeCtlr = new PokemonController(logger.Object, pokesvc.Object);
            
            // Then no Exception is thrown and valid Response Object is returned
            Assert.DoesNotThrowAsync(() => pokeCtlr.GetPokemonDescByName("pikachu"));
            Assert.IsNotNull(pokeCtlr.GetPokemonDescByName("pikachu"));

        }
    }
}
