using System.Collections.Generic;

namespace OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response
{
    public class Assistant
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Instructions { get; set; }
        public List<Tool> Tools { get; set; } = new List<Tool>();
        public List<string> FileIds { get; set; } = new List<string>();
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    }
}
