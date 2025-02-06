using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Files.Request
{
    public class UploadFile
    {
        [JsonPropertyName("file")]
        public byte[] File { get; set; }
        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }
        [JsonIgnore]
        public string Filename { get; set; }
    }
}
