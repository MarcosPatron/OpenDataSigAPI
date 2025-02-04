using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Chat.Request
{
    public class MessageChatCompletionRequest
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
