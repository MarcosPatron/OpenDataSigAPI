﻿namespace OpenDataSigAPI.Data.Entities
{
    public partial class Run
    {
        public decimal Runsid { get; set; }
        public decimal ThreadId { get; set; }
        public decimal AgentId { get; set; }
        public decimal? PromptTokens { get; set; }
        public decimal? CompletionTokens { get; set; }
        public decimal? TotalTokens { get; set; }
        public string Status { get; set; }
        public string IdRun { get; set; }
        public string Model { get; set; }
        public DateTime FechaAlta { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string MotivoBaja { get; set; }
        public DateTime? FechaUltimaModif { get; set; }
        public string UsuarioUltimaModif { get; set; }
        public string AccionUltimaModif { get; set; }
        public string Uuid { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
