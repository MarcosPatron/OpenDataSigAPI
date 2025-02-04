using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Request
{
    public class ToolOutputs
    {
        [JsonPropertyName("tool_call_id")]
        public string ToolCallId { get; set; }
        [JsonPropertyName("output")]
        public string Output { get; set; }
    }
}
