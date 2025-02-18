using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Shared;
using Microsoft.EntityFrameworkCore;
using File = OpenDataSigAPI.Data.Entities.File;
using Shared;

namespace OpenDataSigAPI.Data.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        private readonly IDbContextFactory<OpenDataSigAPIContext> _contextFactory;

        public FilesRepository(IDbContextFactory<OpenDataSigAPIContext> contextFactory)
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

        public async Task Create(File entity, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

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
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                File entidad = await _context.Files.FindAsync(id);
                _context.Files.Remove(entidad);

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

                File entidad = await _context.Files.FindAsync(id);

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

        public async Task<IEnumerable<File>> GetAll()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Files.IgnoreQueryFilters().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<File>> GetAllActive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Files.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<File>> GetAllInactive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var allFiles = await _context.Files.IgnoreQueryFilters().ToListAsync();
                var activeFiles = await _context.Files.ToListAsync();

                return allFiles.Except(activeFiles);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<File> GetById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Files.IgnoreQueryFilters().SingleOrDefaultAsync(m => m.Filesid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<File> Reactivate(decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidad = await _context.Files.IgnoreQueryFilters().SingleOrDefaultAsync(m => m.Filesid == id);

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

        public async Task Update(File entity, decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidadAActualizar = await _context.Files.FindAsync(id);

                entidadAActualizar.AgentId = entity.AgentId;
                entidadAActualizar.Filename = entity.Filename;
                entidadAActualizar.ContentType = entity.ContentType;
                entidadAActualizar.Size = entity.Size;
                entidadAActualizar.FileContent = entity.FileContent;

                entidadAActualizar.UsuarioUltimaModif = user;
                entidadAActualizar.FechaUltimaModif = DateTime.Now;
                entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

                _context.Files.Update(entidadAActualizar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<File>> GetAllActiveByAgentId(decimal agentId)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Files.Where(a=>a.AgentId ==agentId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
