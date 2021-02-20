using NUnit.Framework;
using System.Threading.Tasks;

namespace Pokemon.Tests.IntegrationTest
{
    /// <summary>
    /// This is an integration test to call the Pokemon Controller
    /// </summary>
    [TestFixture]
    public  class PokemonAPITest :  TestWebFactory
    {

        [Test]
        public async Task CallPokemonAPI()
        {
            // Arrange
             var urlPath= "/pokemon/pikachu";

            // Act
            var response = await TestHttpClient.GetAsync(urlPath);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(content);
        }
    }
}
