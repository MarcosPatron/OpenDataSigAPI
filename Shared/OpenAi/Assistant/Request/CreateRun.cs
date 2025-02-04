using System.Text.Json;
using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Request
{
    public class CreateRun
    {
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        [JsonPropertyName("additional_instructions")]
        public string AdditionalInstructions { get; set; }

        [JsonPropertyName("additional_messages")]
        public List<AdditionalMessage> AdditionalMessages { get; set; }

        [JsonPropertyName("tools")]
        public List<Tool> Tools { get; set; }

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }

        [JsonPropertyName("max_prompt_tokens")]
        public int? MaxPromptTokens { get; set; }

        [JsonPropertyName("max_completion_tokens")]
        public int? MaxCompletionTokens { get; set; }

        [JsonPropertyName("truncation_strategy")]
        public TruncationStrategy TruncationStrategy { get; set; }

        [JsonPropertyName("tool_choice")]
        public object ToolChoice { get; set; } //Se indica como object pero puede ser string u object

        [JsonPropertyName("response_format")]
        public object ResponseFormat { get; set; }  //Se indica como object pero puede ser string u object
    }
}
