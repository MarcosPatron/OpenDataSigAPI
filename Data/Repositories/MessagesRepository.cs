using Microsoft.EntityFrameworkCore;
using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Entities;
using Shared;

namespace OpenDataSigAPI.Data.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly IDbContextFactory<OpenDataSigAPIContext> _contextFactory;

        public MessagesRepository(IDbContextFactory<OpenDataSigAPIContext> contextFactory)
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

        public async Task Create(Message entity, string user)
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

                Message entidad = await _context.Messages.FindAsync(id);
                _context.Messages.Remove(entidad);

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

                Message entidad = await _context.Messages.FindAsync(id);

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

        public async Task<IEnumerable<Message>> GetAll()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Messages.IgnoreQueryFilters().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Message>> GetAllActive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Messages.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Message>> GetAllInactive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var allMessages = await _context.Messages.IgnoreQueryFilters().ToListAsync();
                var activeMessages = await _context.Messages.ToListAsync();

                return allMessages.Except(activeMessages);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Message> GetById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Messages.IgnoreQueryFilters().SingleOrDefaultAsync(m => m.Messagesid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Message> Reactivate(decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidad = await _context.Messages.IgnoreQueryFilters().SingleOrDefaultAsync(m => m.Messagesid == id);

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

        public async Task Update(Message entity, decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidadAActualizar = await _context.Messages.FindAsync(id);

                entidadAActualizar.ThreadId = entity.ThreadId;
                entidadAActualizar.Type = entity.Type;
                entidadAActualizar.Content = entity.Content;
                entidadAActualizar.IdMessage = entity.IdMessage;

                entidadAActualizar.UsuarioUltimaModif = user;
                entidadAActualizar.FechaUltimaModif = DateTime.Now;
                entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

                _context.Messages.Update(entidadAActualizar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
