using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Files.Response
{
    public class File
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("bytes")]
        public int Bytes { get; set; }
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }
        [JsonPropertyName("filename")]
        public string Filename { get; set; }
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }
    }
}
