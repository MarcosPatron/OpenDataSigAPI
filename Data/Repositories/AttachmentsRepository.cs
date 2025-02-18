using Microsoft.EntityFrameworkCore;
using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Entities;
using Shared;

namespace OpenDataSigAPI.Data.Repositories
{
    public class AttachmentsRepository : IAttachmentsRepository
    {
        private readonly IDbContextFactory<OpenDataSigAPIContext> _contextFactory;

        public AttachmentsRepository(IDbContextFactory<OpenDataSigAPIContext> contextFactory)
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

        public async Task Create(Attachment entity, string user)
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

                Attachment entidad = await _context.Attachments.FindAsync(id);
                _context.Attachments.Remove(entidad);

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

                Attachment entidad = await _context.Attachments.FindAsync(id);

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

        public async Task<IEnumerable<Attachment>> GetAll()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Attachments.IgnoreQueryFilters().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Attachment>> GetAllActive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Attachments.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Attachment>> GetAllInactive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var allAttachments = await _context.Attachments.IgnoreQueryFilters().ToListAsync();
                var activeAttachments = await _context.Attachments.ToListAsync();

                return allAttachments.Except(activeAttachments);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Attachment> GetById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Attachments.IgnoreQueryFilters().SingleOrDefaultAsync(m => m.Attachmentsid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Attachment> Reactivate(decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidad = await _context.Attachments.IgnoreQueryFilters().SingleOrDefaultAsync(m => m.Attachmentsid == id);

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

        public async Task Update(Attachment entity, decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidadAActualizar = await _context.Attachments.FindAsync(id);

                entidadAActualizar.ThreadId = entity.ThreadId;
                entidadAActualizar.Filename = entity.Filename;
                entidadAActualizar.ContentType = entity.ContentType;
                entidadAActualizar.Size = entity.Size;
                entidadAActualizar.FileContent = entity.FileContent;

                entidadAActualizar.UsuarioUltimaModif = user;
                entidadAActualizar.FechaUltimaModif = DateTime.Now;
                entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

                _context.Attachments.Update(entidadAActualizar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Attachment>> GetAllActiveByThreadId(decimal threadId)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Attachments.Where(a => a.ThreadId == threadId).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
