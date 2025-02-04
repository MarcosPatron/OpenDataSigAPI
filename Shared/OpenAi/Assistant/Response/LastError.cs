using System.Text.Json.Serialization;

namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class LastError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
