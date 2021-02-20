using System.Text.Json.Serialization;

namespace Pokemon.Entities
{
    /// <summary>
    /// This is response object returned when calling the Pokeon API
    /// </summary>
    public class PokemonResponse
    {

        [JsonPropertyName("name")]
        public string PokemonName { get; set; }

        [JsonPropertyName("description")]
        public string ShakespeareDescription { get; set; }
    }
}
