using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Chat.Request
{
    public class ChatCompletionRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("messages")]
        public List<MessageChatCompletionRequest> Messages { get; set; }
        [JsonPropertyName("frequency_penalty")]
        public decimal FrequencyPenalty { get; set; }
        [JsonPropertyName("logit_bias")]
        public Dictionary<string, int> LogitBias { get; set; }
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }
        [JsonPropertyName("n")]
        public int? N { get; set; }
        [JsonPropertyName("presence_penalty")]
        public decimal PresencePenalty { get; set; }
        [JsonPropertyName("response_format")]
        public ResponseFormatChatCompletionRequest ResponseFormat { get; set; }
        [JsonPropertyName("stop")]
        public string Stop { get; set; }
        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }
        [JsonPropertyName("tools")]
        public List<ToolsChatCompletionRequest> Tools { get; set; }
        [JsonPropertyName("user")]
        public string User { get; set; }
    }
}
