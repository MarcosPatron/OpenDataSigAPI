namespace AtencionUsuarios.Data.Entities
{
    public partial class Agent
    {
        public Agent()
        {
            Runs = new HashSet<Run>();
            Files = new HashSet<File>();
        }

        public decimal Agentsid { get; set; }
        public string IdAgent { get; set; }
        public string IdVectorStore { get; set; }
        public string Nombre { get; set; }
        public string Instrucciones { get; set; }
        public string Provider { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public DateTime? FechaUltimaModif { get; set; }
        public string UsuarioUltimaModif { get; set; }
        public string AccionUltimaModif { get; set; }
        public string Uuid { get; set; }

        public virtual ICollection<Run> Runs { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
