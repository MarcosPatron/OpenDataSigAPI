using System.Text.Json.Serialization;

namespace Shared.Models.OpenAi.Assistant.Request
{
    public  class CreateVectorStoreFile
    {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        [JsonPropertyName("chunking_strategy")]
        public ChunkingStrategy ChunkingStrategy { get; set; }
    }
}
