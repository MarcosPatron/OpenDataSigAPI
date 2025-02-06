using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request
{
    public class TruncationStrategy
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }    //auto(por defecto) o last_messages

        [JsonPropertyName("last_messages")]
        public int? LastMessages { get; set; }
    }
}
