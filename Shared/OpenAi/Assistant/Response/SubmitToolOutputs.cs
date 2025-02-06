using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class SubmitToolOutputs
    {
        [JsonPropertyName("tool_calls")]
        public List<ToolCall> ToolCalls { get; set; }
    }
}
