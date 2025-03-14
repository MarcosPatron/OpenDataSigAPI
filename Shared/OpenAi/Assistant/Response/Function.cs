﻿using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class Function
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }
    }
}
