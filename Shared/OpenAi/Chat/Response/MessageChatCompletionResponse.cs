using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Chat.Response
{
    public class MessageChatCompletionResponse
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("tool_calls")]
        public List<ToolCallChatCompletionResponse> ToolCalls { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
