using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Chat.Response
{
    public class FunctionChatCompletionResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }
    }
}
