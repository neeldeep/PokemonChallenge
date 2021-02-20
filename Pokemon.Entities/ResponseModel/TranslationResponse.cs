using System.Text.Json.Serialization;

namespace Pokemon.Entities
{
    /// <summary>
    ///  This object is used to map the response from Translate API
    /// </summary>
    public class TranslationResponse
    {

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }

    public class Contents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("translation")]
        public string Translation { get; set; }
    }
}
