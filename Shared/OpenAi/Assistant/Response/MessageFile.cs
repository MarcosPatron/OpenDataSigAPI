using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class MessageFile
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}
