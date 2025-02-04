using System.Text.Json.Serialization;

namespace Shared.Models.OpenAi.Assistant.Request
{
    public class ExpirationPolicy
    {
        [JsonPropertyName("anchor")]
        public string Anchor { get; set; }

        [JsonPropertyName("days")]
        public int Days { get; set; }
    }
}