using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Chat.Request
{
    public class FunctionChatCompletionRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("parameters")]
        public object Parameters { get; set; }
    }
}
