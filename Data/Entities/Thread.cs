namespace OpenDataSigAPI.Data.Entities
{
    public partial class Thread
    {
        public Thread()
        {
            Messages = new HashSet<Message>();
            Runs = new HashSet<Run>();
            Attachments = new HashSet<Attachment>();
        }

        public decimal Threadsid { get; set; }
        public decimal UserId { get; set; }
        public string Provider { get; set; }
        public string Description { get; set; }
        public decimal? PromptTokens { get; set; }
        public decimal? CompletionTokens { get; set; }
        public decimal? TotalTokens { get; set; }
        public string Status { get; set; }
        public string IdThread { get; set; }
        public string OpinionUsuario { get; set; }
        public string FlagUtil { get; set; }
        public byte? Score { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public DateTime? FechaUltimaModif { get; set; }
        public string UsuarioUltimaModif { get; set; }
        public string AccionUltimaModif { get; set; }
        public string Uuid { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Run> Runs { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
