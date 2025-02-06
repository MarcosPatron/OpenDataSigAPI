using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class Usage
    {
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
