using AtencionUsuarios.Data.Context;
using AtencionUsuarios.Data.Entities;
using AtencionUsuarios.Shared;
using Microsoft.EntityFrameworkCore;

namespace AtencionUsuarios.Data.Repositories
{
    public class RunsRepository : IRunsRepository
    {
        private readonly IDbContextFactory<AtencionUsuariosContext> _contextFactory;

        public RunsRepository(IDbContextFactory<AtencionUsuariosContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private DbContext CreateDbContext()
        {
            try
            {
                return _contextFactory.CreateDbContext();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Run entity, string user)
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();

                if (entity.FechaAlta == DateTime.MinValue)
                    entity.FechaAlta = DateTime.Now;
                entity.UsuarioCreacion = user;

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(decimal id)
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();

                Run entidad = await _context.Runs.FindAsync(id);
                _context.Runs.Remove(entidad);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteLogical(decimal id, string motivoBaja, string user)
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();

                Run entidad = await _context.Runs.FindAsync(id);

                if (entidad == null)
                {
                    throw new Exception("Entidad no encontrada.");
                }

                entidad.FechaBaja = DateTime.Now;
                entidad.MotivoBaja = motivoBaja;
                entidad.UsuarioUltimaModif = user;
                entidad.FechaUltimaModif = DateTime.Now;
                entidad.AccionUltimaModif = Constantes.Operaciones.SOFT_DELETE;

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Run>> GetAll()
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();
                return await _context.Runs.IgnoreQueryFilters().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Run>> GetAllActive()
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();
                return await _context.Runs.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Run>> GetAllInactive()
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();

                var allRuns = await _context.Runs.IgnoreQueryFilters().ToListAsync();
                var activeRuns = await _context.Runs.ToListAsync();

                return allRuns.Except(activeRuns);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Run> GetById(decimal id)
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();
                return await _context.Runs.IgnoreQueryFilters().SingleOrDefaultAsync(r => r.Runsid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Run> Reactivate(decimal id, string user)
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();
                var entidad = await _context.Runs.IgnoreQueryFilters().SingleOrDefaultAsync(r => r.Runsid == id);

                if (entidad == null)
                {
                    return null;
                }

                entidad.FechaBaja = null;
                entidad.MotivoBaja = null;
                entidad.UsuarioUltimaModif = user;
                entidad.FechaUltimaModif = DateTime.Now;
                entidad.AccionUltimaModif = Constantes.Operaciones.REACTIVATE;

                await _context.SaveChangesAsync();
                return entidad;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Run entity, decimal id, string user)
        {
            try
            {
                var _context = (AtencionUsuariosContext)CreateDbContext();
                var entidadAActualizar = await _context.Runs.FindAsync(id);

                entidadAActualizar.ThreadId = entity.ThreadId;
                entidadAActualizar.AgentId = entity.AgentId;
                entidadAActualizar.PromptTokens = entity.PromptTokens;
                entidadAActualizar.CompletionTokens = entity.CompletionTokens;
                entidadAActualizar.TotalTokens = entity.TotalTokens;
                entidadAActualizar.Status = entity.Status;
                entidadAActualizar.IdRun = entity.IdRun;
                entidadAActualizar.Model = entity.Model;

                entidadAActualizar.UsuarioUltimaModif = user;
                entidadAActualizar.FechaUltimaModif = DateTime.Now;
                entidadAActualizar.AccionUltimaModif = Constantes.Operaciones.UPDATE;

                _context.Runs.Update(entidadAActualizar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
