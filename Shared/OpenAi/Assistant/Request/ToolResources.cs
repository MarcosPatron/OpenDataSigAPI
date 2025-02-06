using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request
{
    public class ToolResources
    {

    }

    public class CodeInterpreterResourcesDto : ToolResources
    {
        [JsonPropertyName("file_ids")]
        public List<string> FileIds { get; set; }
    }

    public class FileSearchResourcesDto : ToolResources
    {
        [JsonPropertyName("vector_store_ids")]
        public List<string> VectorStoreIds { get; set; }
    }
}
