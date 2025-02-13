using Microsoft.EntityFrameworkCore;
using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Entities;
using Shared;

namespace OpenDataSigAPI.Data.Repositories
{
    public class RunsRepository : IRunsRepository
    {
        private readonly IDbContextFactory<OpenDataSigAPIContext> _contextFactory;

        public RunsRepository(IDbContextFactory<OpenDataSigAPIContext> contextFactory)
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

        public async Task Create(Run entity)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                if (entity.FechaAlta == DateTime.MinValue)
                    entity.FechaAlta = DateTime.Now;

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
                var _context = (OpenDataSigAPIContext)CreateDbContext();

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
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                Run entidad = await _context.Runs.FindAsync(id);

                if (entidad == null)
                {
                    throw new Exception("Entidad no encontrada.");
                }

                entidad.FechaBaja = DateTime.Now;
                entidad.MotivoBaja = motivoBaja;
                entidad.UsuarioUltimaModif = user;
                entidad.FechaUltimaModif = DateTime.Now;
                entidad.AccionUltimaModif = Constants.Operaciones.SOFT_DELETE;

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
                var _context = (OpenDataSigAPIContext)CreateDbContext();
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
                var _context = (OpenDataSigAPIContext)CreateDbContext();
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
                var _context = (OpenDataSigAPIContext)CreateDbContext();

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
                var _context = (OpenDataSigAPIContext)CreateDbContext();
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
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidad = await _context.Runs.IgnoreQueryFilters().SingleOrDefaultAsync(r => r.Runsid == id);

                if (entidad == null)
                {
                    return null;
                }

                entidad.FechaBaja = null;
                entidad.MotivoBaja = null;
                entidad.UsuarioUltimaModif = user;
                entidad.FechaUltimaModif = DateTime.Now;
                entidad.AccionUltimaModif = Constants.Operaciones.REACTIVATE;

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
                var _context = (OpenDataSigAPIContext)CreateDbContext();
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
                entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

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
