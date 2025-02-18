using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Entities;
using OpenDataSigAPI.Shared;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace OpenDataSigAPI.Data.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        private readonly IDbContextFactory<OpenDataSigAPIContext> _contextFactory;

        public LogsRepository(IDbContextFactory<OpenDataSigAPIContext> contextFactory)
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

        public async Task Create(Log entity, string user)
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

                Log entidad = await _context.Logs.FindAsync(id);
                _context.Logs.Remove(entidad);

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

                Log entidad = await _context.Logs.FindAsync(id);

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

        public async Task<IEnumerable<Log>> GetAll()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Logs.IgnoreQueryFilters().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Log>> GetAllActive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Logs.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Log>> GetAllInactive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var allLogs = await _context.Logs.IgnoreQueryFilters().ToListAsync();
                var activeLogs = await _context.Logs.ToListAsync();

                return allLogs.Except(activeLogs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Log> GetById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Logs.IgnoreQueryFilters().SingleOrDefaultAsync(a => a.Logsid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Log> Reactivate(decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidad = await _context.Logs.IgnoreQueryFilters().SingleOrDefaultAsync(a => a.Logsid == id);

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

        public async Task Update(Log entity, decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidadAActualizar = await _context.Logs.FindAsync(id);

                entidadAActualizar.Mensaje = entity.Mensaje;
                entidadAActualizar.Descripcion = entity.Descripcion;
                entidadAActualizar.Objeto = entity.Objeto;
                entidadAActualizar.Metodo = entity.Metodo;
                entidadAActualizar.TipoLog = entity.TipoLog;

                entidadAActualizar.UsuarioUltimaModif = user;
                entidadAActualizar.FechaUltimaModif = DateTime.Now;
                entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

                _context.Logs.Update(entidadAActualizar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogFatal(string objeto, string metodo, string message, string stackTrace, Exception innerException, string user)
        {
            try
            {
                var nuevoError = new Log();
                nuevoError.Objeto = objeto.Length < 10 ? objeto : objeto.Substring(0, 10);
                nuevoError.Metodo = metodo.Length < 100 ? metodo : metodo.Substring(0, 100);
                nuevoError.Mensaje = message.Length < 1000 ? message : message.Substring(0, 1000);
                nuevoError.Descripcion = innerException == null ? stackTrace : $"{stackTrace} - Inner:{innerException.Message}";
                if (nuevoError.Descripcion.Length > 3000) { nuevoError.Descripcion = nuevoError.Descripcion.Substring(0, 3000); }
                nuevoError.TipoLog = Constants.TiposLogs.FATAL;

                await Create(nuevoError, user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogInfo(string objeto, string metodo, string message, string descripcion, string user)
        {
            try
            {
                var nuevoInfo = new Log();
                nuevoInfo.Objeto = objeto.Length < 10 ? objeto : objeto.Substring(0, 10);
                nuevoInfo.Metodo = metodo.Length < 100 ? metodo : metodo.Substring(0, 100);
                nuevoInfo.Mensaje = message.Length < 1000 ? message : message.Substring(0, 1000);
                nuevoInfo.Descripcion = descripcion.Length < 1000 ? descripcion : descripcion.Substring(0, 3000);
                nuevoInfo.TipoLog = Constants.TiposLogs.INFO;

                await Create(nuevoInfo, user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogWarning(string objeto, string metodo, string message, string descripcion, string user)
        {
            var nuevoWarning = new Log();
            nuevoWarning.Objeto = objeto.Length < 10 ? objeto : objeto.Substring(0, 10);
            nuevoWarning.Metodo = metodo.Length < 100 ? metodo : metodo.Substring(0, 100);
            nuevoWarning.Mensaje = message.Length < 1000 ? message : message.Substring(0, 1000);
            nuevoWarning.Descripcion = descripcion.Length < 1000 ? descripcion : descripcion.Substring(0, 3000);
            nuevoWarning.TipoLog = Constants.TiposLogs.WARNING;

            await Create(nuevoWarning, user);
        }

        public async Task LogError(string objeto, string metodo, string message, string descripcion, string user)
        {
            var nuevoWarning = new Log();
            nuevoWarning.Objeto = objeto.Length < 10 ? objeto : objeto.Substring(0, 10);
            nuevoWarning.Metodo = metodo.Length < 100 ? metodo : metodo.Substring(0, 100);
            nuevoWarning.Mensaje = message.Length < 1000 ? message : message.Substring(0, 1000);
            nuevoWarning.Descripcion = descripcion.Length < 1000 ? descripcion : descripcion.Substring(0, 3000);
            nuevoWarning.TipoLog = Constants.TiposLogs.ERROR;

            await Create(nuevoWarning, user);
        }
    }
}
