using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class Message
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }
        [JsonPropertyName("thread_id")]
        public string ThreadId { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public List<ContentItem> Content { get; set; }
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }
        [JsonPropertyName("run_id")]
        public string RunId { get; set; }
        [JsonPropertyName("file_ids")]
        public List<string> FileIds { get; set; }
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
