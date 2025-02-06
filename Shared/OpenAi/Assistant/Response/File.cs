using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class File
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("id")]
        public int Bytes { get; set; }
        [JsonPropertyName("id")]
        public long CreatedAt { get; set; }
        [JsonPropertyName("created_at")]
        public string Filename { get; set; }
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }
    }
}
