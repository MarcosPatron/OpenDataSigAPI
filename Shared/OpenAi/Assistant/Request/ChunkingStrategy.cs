using System.Text.Json.Serialization;

namespace Shared.Models.OpenAi.Assistant.Request
{
    public class ChunkingStrategy
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("static")]
        public ChunkingStrategyStatic ChunkingStrategyStatic { get; set; }
    }
}