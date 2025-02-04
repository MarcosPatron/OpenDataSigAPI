using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class FilePath
    {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }
    }
}
