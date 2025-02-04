using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class TextContent
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("annotations")]
        public List<Annotation> Annotations { get; set; }
    }
}
