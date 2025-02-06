using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class ToolCall
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } // For now, always "function"
        [JsonPropertyName("function")]
        public Function Function { get; set; }
    }
}
