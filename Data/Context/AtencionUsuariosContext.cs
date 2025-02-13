using AtencionUsuarios.Data.Entities;
using Microsoft.EntityFrameworkCore;
using File = AtencionUsuarios.Data.Entities.File;

namespace AtencionUsuarios.Data.Context
{
    public partial class AtencionUsuariosContext : DbContext
    {
        public AtencionUsuariosContext()
        {
        }

        public AtencionUsuariosContext(DbContextOptions<AtencionUsuariosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Message> Messages { get; set; } 
        public virtual DbSet<Run> Runs { get; set; } 
        public virtual DbSet<Entities.Thread> Threads { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Log> Logs { get; set; } 
        public virtual DbSet<Attachment> Attachments { get; set; } 
        public virtual DbSet<File> Files { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ATENCION_USUARIOS")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.Agentsid)
                    .HasName("SYS_C007707379");

                entity.ToTable("AGENTS");

                entity.Property(e => e.Agentsid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AGENTSID")
                    .HasDefaultValueSql("\"ATENCION_USUARIOS\".\"SECUENCIA_AGENTS\".\"NEXTVAL\"");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.IdAgent)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_AGENT");

                entity.Property(e => e.IdVectorStore)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_VECTOR_STORE");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE ");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.Instrucciones)
                    .HasColumnType("CLOB")
                    .HasColumnName("INSTRUCCIONES");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Provider)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PROVIDER");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system' ");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid() ");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Messagesid)
                    .HasName("SYS_C007707404");

                entity.ToTable("MESSAGES");

                entity.Property(e => e.Messagesid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MESSAGESID")
                    .HasDefaultValueSql("\"ATENCION_USUARIOS\".\"SECUENCIA_MESSAGES\".\"NEXTVAL\"");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.Content)
                    .HasColumnType("CLOB")
                    .HasColumnName("CONTENT");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE ");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.IdMessage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_MESSAGE");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.ThreadId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("THREAD_ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system' ");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid() ");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007707405");
            });

            modelBuilder.Entity<Run>(entity =>
            {
                entity.HasKey(e => e.Runsid)
                    .HasName("SYS_C007707396");

                entity.ToTable("RUNS");

                entity.Property(e => e.Runsid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RUNSID")
                    .HasDefaultValueSql("\"ATENCION_USUARIOS\".\"SECUENCIA_RUNS\".\"NEXTVAL\"");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.AgentId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AGENT_ID");

                entity.Property(e => e.CompletionTokens)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPLETION_TOKENS");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE ");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.IdRun)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_RUN");

                entity.Property(e => e.Model)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("MODEL");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.PromptTokens)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROMPT_TOKENS");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.ThreadId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("THREAD_ID");

                entity.Property(e => e.TotalTokens)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTAL_TOKENS");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system' ");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid() ");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Runs)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007707398");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Runs)
                    .HasForeignKey(d => d.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007707397");
            });

            modelBuilder.Entity<Entities.Thread>(entity =>
            {
                entity.HasKey(e => e.Threadsid)
                    .HasName("SYS_C007707387");

                entity.ToTable("THREADS");

                entity.Property(e => e.Threadsid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("THREADSID")
                    .HasDefaultValueSql("\"ATENCION_USUARIOS\".\"SECUENCIA_THREADS\".\"NEXTVAL\"");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.CompletionTokens)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPLETION_TOKENS");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE ");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.FlagUtil)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FLAG_UTIL")
                    .HasDefaultValueSql("'0' ");

                entity.Property(e => e.IdThread)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_THREAD");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.OpinionUsuario)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("OPINION_USUARIO");

                entity.Property(e => e.PromptTokens)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROMPT_TOKENS");

                entity.Property(e => e.Provider)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PROVIDER");

                entity.Property(e => e.Score)
                    .HasPrecision(1)
                    .HasColumnName("SCORE");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.TotalTokens)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTAL_TOKENS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system' ");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid() ");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Threads)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007707388");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Usersid)
                    .HasName("SYS_C007707371");

                entity.ToTable("USERS");

                entity.HasIndex(e => e.Username, "SYS_C007707372")
                    .IsUnique();

                entity.Property(e => e.Usersid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERSID")
                    .HasDefaultValueSql("\"ATENCION_USUARIOS\".\"SECUENCIA_USERS\".\"NEXTVAL\"");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.Avatar)
                    .HasColumnType("BLOB")
                    .HasColumnName("AVATAR");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE ");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_COMPLETO");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system' ");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid() ");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.Logsid)
                    .HasName("SYS_C003625364");

                entity.ToTable("LOGS");

                entity.Property(e => e.Logsid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LOGSID")
                    .HasDefaultValueSql("SECUENCIA_LOGS.NEXTVAL");

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MENSAJE");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Objeto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OBJETO");

                entity.Property(e => e.Metodo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("METODO");

                entity.Property(e => e.TipoLog)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_LOG");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system'");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid()");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("ATTACHMENTS");

                entity.HasKey(e => e.Attachmentsid)
                    .HasName("SYS_C007751424");

                entity.Property(e => e.Attachmentsid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ATTACHMENTSID")
                    .HasDefaultValueSql("SECUENCIA_ATTACHMENTS.NEXTVAL");

                entity.Property(e => e.ThreadId)
                    .HasColumnType("DECIMAL")
                    .HasColumnName("THREAD_ID")
                    .IsRequired();

                entity.Property(e => e.Filename)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FILENAME")
                    .IsRequired();

                entity.Property(e => e.ContentType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONTENT_TYPE")
                    .IsRequired();

                entity.Property(e => e.FileContent)
                    .HasColumnName("FILE_CONTENT")
                    .HasColumnType("BLOB")
                    .IsRequired();

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE ")
                    .IsRequired();

                entity.Property(e => e.Size)
                    .HasColumnType("NUMBER(19,0)")
                    .HasColumnName("SIZE")
                    .IsRequired();

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system' ")
                    .IsRequired();

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid() ")
                    .IsRequired();

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_THREAD_ID");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("FILES");

                entity.HasKey(e => e.Filesid)
                    .HasName("SYS_C007809033");

                entity.Property(e => e.Filesid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FILESID")
                    .HasDefaultValueSql("SECUENCIA_FILES.NEXTVAL");

                entity.Property(e => e.AgentId)
                    .HasColumnType("DECIMAL")
                    .HasColumnName("AGENT_ID")
                    .IsRequired();

                entity.Property(e => e.IdFile)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_FILE");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FILENAME");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONTENT_TYPE");

                entity.Property(e => e.Size)
                    .HasColumnType("NUMBER(19,0)")
                    .HasColumnName("SIZE")
                    .IsRequired();

                entity.Property(e => e.FileContent)
                    .HasColumnType("BLOB")
                    .HasColumnName("FILE_CONTENT")
                    .IsRequired();

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ALTA")
                    .HasDefaultValueSql("SYSDATE")
                    .IsRequired();

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION")
                    .HasDefaultValueSql("'system'");

                entity.Property(e => e.FechaBaja)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_BAJA");

                entity.Property(e => e.MotivoBaja)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO_BAJA");

                entity.Property(e => e.FechaUltimaModif)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ULTIMA_MODIF");

                entity.Property(e => e.UsuarioUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_ULTIMA_MODIF");

                entity.Property(e => e.AccionUltimaModif)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCION_ULTIMA_MODIF");

                entity.Property(e => e.Uuid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("sys_guid()");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FILES_AGENT_ID");
            });


            modelBuilder.HasSequence("SECUENCIA_AGENTS");

            modelBuilder.HasSequence("SECUENCIA_MESSAGES");

            modelBuilder.HasSequence("SECUENCIA_RUNS");

            modelBuilder.HasSequence("SECUENCIA_THREADS");

            modelBuilder.HasSequence("SECUENCIA_USERS");

            modelBuilder.HasSequence("SECUENCIA_LOGS");

            modelBuilder.HasSequence("SECUENCIA_ATTACHMENTS");

            modelBuilder.HasSequence("SECUENCIA_FILES");

            modelBuilder.Entity<Agent>().HasQueryFilter(entity => entity.FechaBaja == null);
            modelBuilder.Entity<Message>().HasQueryFilter(entity => entity.FechaBaja == null);
            modelBuilder.Entity<Run>().HasQueryFilter(entity => entity.FechaBaja == null);
            modelBuilder.Entity<Entities.Thread>().HasQueryFilter(entity => entity.FechaBaja == null);
            modelBuilder.Entity<User>().HasQueryFilter(entity => entity.FechaBaja == null);
            modelBuilder.Entity<Log>().HasQueryFilter(entity => entity.FechaBaja == null);
            modelBuilder.Entity<Attachment>().HasQueryFilter(entity => entity.FechaBaja == null);
            modelBuilder.Entity<Entities.File>().HasQueryFilter(entity => entity.FechaBaja == null);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
