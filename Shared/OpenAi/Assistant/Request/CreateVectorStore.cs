using System.Text.Json.Serialization;

namespace Shared.Models.OpenAi.Assistant.Request
{
    public class CreateVectorStore
    {
        [JsonPropertyName("file_ids")]
        public List<string> FileIds { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("expires_after")]
        public ExpirationPolicy ExpiresAfter { get; set; }

        [JsonPropertyName("chunking_strategy")]
        public ChunkingStrategy ChunkingStrategy { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
