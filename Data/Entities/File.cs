namespace AtencionUsuarios.Data.Entities
{
    public class File
    {
        public decimal Filesid { get; set; }
        public decimal AgentId { get; set; }
        public string IdFile { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public DateTime? FechaUltimaModif { get; set; }
        public string UsuarioUltimaModif { get; set; }
        public string AccionUltimaModif { get; set; }
        public string Uuid { get; set; }

        public virtual Agent Agent { get; set; }
    }
}
