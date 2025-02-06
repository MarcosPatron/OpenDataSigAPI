using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request
{
    public class RetrieveRun
    {
        [JsonPropertyName("thread_id")]
        public string ThreadId { get; set; }
        [JsonPropertyName("run_id")]
        public string RunId { get; set; }
    }
}
