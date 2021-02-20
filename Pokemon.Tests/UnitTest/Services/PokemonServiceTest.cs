using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PokeApiNet;
using Pokemon.Entities;
using Pokemon.Services;
using Pokemon.Services.Interfaces;
using System.Collections.Generic;

namespace Pokemon.Tests.Services
{
    public class PokemonServiceTest
    {
        Mock<ILogger<PokemonService>> logger;
       
        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<PokemonService>>();
        }

        [Test]
        public void TestPokemonValidNameNoException()
        {
            // Given a Valid PokemonName
            var vaidPokemonName = "pikachu";
            // Sample Pokemon Object used in Test
            var pokemonSpecies = new PokemonSpecies() 
            { Name = vaidPokemonName, 
                FlavorTextEntries= new List<PokemonSpeciesFlavorTexts>() 
                { new PokemonSpeciesFlavorTexts() { FlavorText="Sample Flavor Text",
                                                    Language=new NamedApiResource<Language>(){ Name="en"} } } };
            // Setup of Mock Objects 
            var mockTranslateSvc = new Mock<ITranslateService>();
            mockTranslateSvc.Setup(obj => obj.GetShakespeareText(It.IsAny<string>())).ReturnsAsync("some translated text");
            var pokemanClient = new Mock<IPokeAPIClient>();
            pokemanClient.Setup(obj => obj.GetPokemonByName(It.IsAny<string>())).ReturnsAsync(pokemonSpecies);

            //When calling the Pokemon Service
            var pokeService = new PokemonService(logger.Object, mockTranslateSvc.Object, pokemanClient.Object);

            // Then no Exception is returned
            Assert.DoesNotThrowAsync(() => pokeService.GetPokemonDescByName(vaidPokemonName));
        }

        [Test]
        public void TestPokemonValidButNoDescription()
        {
            // Given a Valid PokemonName But No Description Returned from Poke API
            var vaidPokemonName = "pikachu";
            // Sample Pokemon Object used in Test
            var pokemonSpecies = new PokemonSpecies()
            {
                Name = vaidPokemonName,
            };
            // Setup of Mock Objects 
            var mockTranslateSvc = new Mock<ITranslateService>();
            mockTranslateSvc.Setup(obj => obj.GetShakespeareText(It.IsAny<string>())).ReturnsAsync("some translated text");
            var pokemanClient = new Mock<IPokeAPIClient>();
            pokemanClient.Setup(obj => obj.GetPokemonByName(It.IsAny<string>())).ReturnsAsync(pokemonSpecies);

            //When calling the Pokemon Service
            var pokeService = new PokemonService(logger.Object, mockTranslateSvc.Object, pokemanClient.Object);
            // Pokemon API Exception is Returned
            Assert.ThrowsAsync<APIException>(() => pokeService.GetPokemonDescByName(vaidPokemonName));
        }

    }
}
