using System.Text.Json.Serialization;

namespace Pokemon.Entities
{
    public class PokemonResponse
    {

        [JsonPropertyName("name")]
        public string PokemonName { get; set; }

        [JsonPropertyName("description")]
        public string ShakespeareDescription { get; set; }
    }
}
