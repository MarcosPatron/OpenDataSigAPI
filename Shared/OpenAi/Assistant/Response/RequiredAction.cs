using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class RequiredAction
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } //For now, this is always submit_tool_outputs.
        [JsonPropertyName("submit_tool_outputs")]
        public SubmitToolOutputs SubmitToolOutputs { get; set; }
    }
}
