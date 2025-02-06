using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request
{
    public class Attachment
    {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        [JsonPropertyName("tools")]
        public List<Tool> Tools { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
