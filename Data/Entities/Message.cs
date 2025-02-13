namespace AtencionUsuarios.Data.Entities
{
    public partial class Message
    {
        public decimal Messagesid { get; set; }
        public decimal ThreadId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string IdMessage { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public DateTime? FechaUltimaModif { get; set; }
        public string UsuarioUltimaModif { get; set; }
        public string AccionUltimaModif { get; set; }
        public string Uuid { get; set; }

        public virtual Thread Thread { get; set; }
    }
}
