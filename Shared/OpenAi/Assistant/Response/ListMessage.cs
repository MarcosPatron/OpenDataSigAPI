using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class ListMessage
    {
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("data")]
        public List<Message> Messages { get; set; }
        [JsonPropertyName("first_id")]
        public string FirstId { get; set; }
        [JsonPropertyName("last_id")]
        public string LastId { get; set; }
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }
    }
}
