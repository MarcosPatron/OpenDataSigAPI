using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request
{
    public class SubmitToolOutputs
    {
        [JsonPropertyName("tool_outputs")]
        public List<ToolOutputs> ToolOutputs { get; set; }
    }
}
