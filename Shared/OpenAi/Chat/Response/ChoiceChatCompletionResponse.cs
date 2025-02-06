using System.Text.Json.Serialization;


namespace OpenDataSigAPI.Shared.Models.OpenAi.Chat.Response
{
    public class ChoiceChatCompletionResponse
    {
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("message")]
        public MessageChatCompletionResponse Message { get; set; }
    }
}
