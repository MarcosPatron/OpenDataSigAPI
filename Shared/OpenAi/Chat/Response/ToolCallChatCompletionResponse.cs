using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Chat.Response
{
    public class ToolCallChatCompletionResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("function")]
        public FunctionChatCompletionResponse Function { get; set; }
    }
}
