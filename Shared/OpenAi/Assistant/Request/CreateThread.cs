using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Request
{
    public class CreateThread
    {
        [JsonPropertyName("messages")]
        public List<CreateMessage> Messages { get; set; }

        [JsonPropertyName("tool_resources")]
        public Dictionary<string, ToolResources> ToolResources { get; set; }  //La clave del diccionario sería code_interpreter o file_search

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
