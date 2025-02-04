using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class Run
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }
        [JsonPropertyName("thread_id")]
        public string ThreadId { get; set; }
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("required_action")]
        public RequiredAction RequiredAction { get; set; }
        [JsonPropertyName("last_error")]
        public LastError LastError { get; set; }
        [JsonPropertyName("expires_at")]
        public long? ExpiresAt { get; set; }
        [JsonPropertyName("started_at")]
        public long? StartedAt { get; set; }
        [JsonPropertyName("cancelled_at")]
        public long? CancelledAt { get; set; }
        [JsonPropertyName("failed_at")]
        public long? FailedAt { get; set; }
        [JsonPropertyName("completed_at")]
        public long? CompletedAt { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }
        [JsonPropertyName("tools")]
        public List<Tool> Tools { get; set; } = new List<Tool>();
        [JsonPropertyName("file_ids")]
        public List<string> FileIds { get; set; } = new List<string>();
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }
}
