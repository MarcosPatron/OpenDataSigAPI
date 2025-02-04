using System.Text.Json.Serialization;

namespace Shared.Models.OpenAi.Assistant.Response
{
    public class VectorStore
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("usage_bytes")]
        public int UsageBytes { get; set; }

        [JsonPropertyName("file_counts")]
        public FileCounts FileCounts { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("expires_after")]
        public ExpirationPolicy ExpiresAfter { get; set; }

        [JsonPropertyName("expires_at")]
        public int? ExpiresAt { get; set; }

        [JsonPropertyName("last_active_at")]
        public int? LastActiveAt { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
