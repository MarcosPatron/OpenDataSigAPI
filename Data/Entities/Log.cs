namespace AtencionUsuarios.Data.Entities
{
    public partial class Log
    {
        public decimal Logsid { get; set; }
        public string Mensaje { get; set; }
        public string Descripcion { get; set; }
        public string Objeto { get; set; }
        public string Metodo { get; set; }
        public string TipoLog { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public DateTime? FechaUltimaModif { get; set; }
        public string UsuarioUltimaModif { get; set; }
        public string AccionUltimaModif { get; set; }
        public string Uuid { get; set; }
    }
}
