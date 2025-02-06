using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Chat.Request
{
    public class ToolsChatCompletionRequest
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("function")]
        public FunctionChatCompletionRequest Function { get; set; }
    }
}
