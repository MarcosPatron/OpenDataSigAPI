﻿using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class Annotation
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // "file_citation"
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("file_citation")]
        public FileCitation FileCitation { get; set; }
        [JsonPropertyName("file_path")]
        public FilePath FilePath { get; set; }
        [JsonPropertyName("start_index")]
        public int StartIndex { get; set; }
        [JsonPropertyName("end_index")]
        public int EndIndex { get; set; }
    }
}
