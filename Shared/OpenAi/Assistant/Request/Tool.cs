using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Request
{
    public class Tool
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class CodeInterpreterToolDto : Tool
    {
        public CodeInterpreterToolDto()
        {
            Type = "code_interpreter";
        }
    }

    public class FileSearchToolDto : Tool
    {
        public FileSearchToolDto()
        {
            Type = "file_search";
        }
    }

    public class FunctionToolDto : Tool
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("parameters")]
        public object Parameters { get; set; } // Consider using a more specific type or structure to represent JSON Schema

        public FunctionToolDto()
        {
            Type = "function";
        }
    }
}
