﻿using System.Text.Json.Serialization;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request
{
    public class CreateThreadAndRun
    {
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        [JsonPropertyName("thread")]
        public CreateThread Thread { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        [JsonPropertyName("tools")]
        public List<Tool> Tools { get; set; }

        [JsonPropertyName("tool_resources")]
        public Dictionary<string, ToolResources> ToolResources { get; set; }  //La clave del diccionario sería code_interpreter o file_search

        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }

        [JsonPropertyName("max_prompt_tokens")]
        public int? MaxPromptTokens { get; set; }

        [JsonPropertyName("max_completion_tokens")]
        public int? MaxCompletionTokens { get; set; }

        [JsonPropertyName("truncation_strategy")]
        public TruncationStrategy TruncationStrategy { get; set; }

        [JsonPropertyName("tool_choice")]
        public object ToolChoice { get; set; } //Se indica como object pero puede ser string u object

        [JsonPropertyName("response_format")]
        public object ResponseFormatString { get; set; }   //Se indica como object pero puede ser string u object
    }
}
