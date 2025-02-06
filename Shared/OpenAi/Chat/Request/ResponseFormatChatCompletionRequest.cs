using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Chat.Request
{
    public class ResponseFormatChatCompletionRequest
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
