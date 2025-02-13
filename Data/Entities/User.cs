namespace AtencionUsuarios.Data.Entities
{
    public partial class User
    {
        public User()
        {
            Threads = new HashSet<Thread>();
        }

        public decimal Usersid { get; set; }
        public string Username { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public byte[] Avatar { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public DateTime? FechaUltimaModif { get; set; }
        public string UsuarioUltimaModif { get; set; }
        public string AccionUltimaModif { get; set; }
        public string Uuid { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }
    }
}
