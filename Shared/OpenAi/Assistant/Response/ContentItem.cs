using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class ContentItem
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // "text" or "image_file"
        [JsonPropertyName("text")]
        public TextContent Text { get; set; }
        [JsonPropertyName("image_file")]
        public ImageFile ImageFile { get; set; }
    }
}
