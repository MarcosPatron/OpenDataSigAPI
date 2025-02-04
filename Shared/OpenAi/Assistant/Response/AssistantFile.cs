namespace AtencionUsuarios.Shared.Models.OpenAi.Assistant.Response
{
    public class AssistantFile
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long CreatedAt { get; set; }
        public string AssistantId { get; set; }
    }
}
