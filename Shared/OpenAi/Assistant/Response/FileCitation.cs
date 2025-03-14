﻿using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class FileCitation
    {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }
        [JsonPropertyName("quote")]
        public string Quote { get; set; }
    }
}
