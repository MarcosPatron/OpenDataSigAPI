using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request
{
    public class ToolChoice
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // Debería ser "function", "file_search", "code_interpreter".
    }

    public class CodeInterpreterToolChoiceDto : ToolChoice
    {
        public CodeInterpreterToolChoiceDto()
        {
            Type = "code_interpreter";
        }
    }

    public class FileSearchToolChoiceDto : ToolChoice
    {
        public FileSearchToolChoiceDto()
        {
            Type = "file_search";
        }
    }

    public class FunctionToolChoiceDto : ToolChoice
    {
        [JsonPropertyName("function")]
        public FunctionDetailsDto FunctionDetails { get; set; }

        public FunctionToolChoiceDto()
        {
            Type = "function";
        }
    }

    public class FunctionDetailsDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
