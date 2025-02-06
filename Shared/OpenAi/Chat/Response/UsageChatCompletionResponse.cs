using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Chat.Response
{
    public class UsageChatCompletionResponse
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
