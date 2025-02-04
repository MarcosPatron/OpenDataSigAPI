using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Request
{
    public class ResponseFormat
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // Debería ser "text " o  "json_object"
    }
}
