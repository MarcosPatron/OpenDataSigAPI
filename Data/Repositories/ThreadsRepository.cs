using Microsoft.EntityFrameworkCore;
using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using Shared;

namespace OpenDataSigAPI.Data.Repositories
{
    public class ThreadsRepository : IThreadsRepository
    {
        private readonly IDbContextFactory<OpenDataSigAPIContext> _contextFactory;

        public ThreadsRepository(IDbContextFactory<OpenDataSigAPIContext> contextFactory)
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

        public async Task Create(OpenDataSigAPI.Data.Entities.Thread entity)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                if (entity.FechaAlta == DateTime.MinValue)
                    entity.FechaAlta = DateTime.Now;

                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var sqlEx = ex.InnerException as OracleException;
                if (sqlEx != null)
                {
                    // Registra información detallada del error de Oracle
                    Console.WriteLine($"Oracle Error Code: {sqlEx.Number}");
                    Console.WriteLine($"Oracle Error Message: {sqlEx.Message}");
                }
                else
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                throw;
            }
        }

        public async Task Delete(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                OpenDataSigAPI.Data.Entities.Thread entidad = await _context.Threads.FindAsync(id);
                _context.Threads.Remove(entidad);

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

                OpenDataSigAPI.Data.Entities.Thread entidad = await _context.Threads.FindAsync(id);

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

        public async Task<IEnumerable<OpenDataSigAPI.Data.Entities.Thread>> GetAll()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.IgnoreQueryFilters().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OpenDataSigAPI.Data.Entities.Thread>> GetAllActive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<OpenDataSigAPI.Data.Entities.Thread>> GetAllInactive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var allThreads = await _context.Threads.IgnoreQueryFilters().ToListAsync();
                var activeThreads = await _context.Threads.ToListAsync();

                return allThreads.Except(activeThreads);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OpenDataSigAPI.Data.Entities.Thread> GetById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.IgnoreQueryFilters().SingleOrDefaultAsync(t => t.Threadsid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OpenDataSigAPI.Data.Entities.Thread> GetThreadWithAttachmentsById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var thread = await _context.Threads.Include(t => t.Attachments.OrderBy(a => a.FechaAlta)).SingleOrDefaultAsync(t => t.Threadsid == id);

                if (thread != null && thread.Attachments == null)
                {
                    thread.Attachments = new List<Attachment>();
                }

                return thread;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OpenDataSigAPI.Data.Entities.Thread> GetThreadWithMessagesAndAttachmentsById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.Include(t => t.Messages.OrderBy(m => m.FechaAlta)).Include(t => t.Attachments.OrderBy(a => a.FechaAlta)).SingleOrDefaultAsync(t => t.Threadsid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Entities.Thread> GetThreadWithMessagesAndAttachmentsAndUserById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.Include(t => t.Messages.OrderBy(m => m.FechaAlta))
                                             .Include(t => t.Attachments.OrderBy(a => a.FechaAlta))
                                             .Include(t => t.User)
                                             .SingleOrDefaultAsync(t => t.Threadsid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OpenDataSigAPI.Data.Entities.Thread> Reactivate(decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidad = await _context.Threads.IgnoreQueryFilters().SingleOrDefaultAsync(t => t.Threadsid == id);

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

        public async Task Update(OpenDataSigAPI.Data.Entities.Thread entity, decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidadAActualizar = await _context.Threads.FindAsync(id);

                entidadAActualizar.UserId = entity.UserId;
                entidadAActualizar.Provider = entity.Provider;
                entidadAActualizar.Description = entity.Description;
                entidadAActualizar.PromptTokens = entity.PromptTokens;
                entidadAActualizar.CompletionTokens = entity.CompletionTokens;
                entidadAActualizar.TotalTokens = entity.TotalTokens;
                entidadAActualizar.Status = entity.Status;
                entidadAActualizar.IdThread = entity.IdThread;
                entidadAActualizar.OpinionUsuario = entity.OpinionUsuario;
                entidadAActualizar.FlagUtil = entity.FlagUtil;
                entidadAActualizar.Score = entity.Score;

                entidadAActualizar.UsuarioUltimaModif = user;
                entidadAActualizar.FechaUltimaModif = DateTime.Now;
                entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

                _context.Threads.Update(entidadAActualizar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetIdThreadByThreadId(decimal threadId)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                var thread = await context.Threads.IgnoreQueryFilters().SingleOrDefaultAsync(t => t.Threadsid == threadId);
                return thread?.IdThread ?? string.Empty;
            }

        }

        public async Task<Entities.Thread> UpdateThreadStatusAndId(decimal threadId, string threadStatus, string idThread, string user)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                var thread = await context.Threads.FindAsync(threadId);

                thread.Status = threadStatus;
                thread.IdThread = idThread;

                thread.UsuarioUltimaModif = user;
                thread.FechaUltimaModif = DateTime.Now;
                thread.AccionUltimaModif = Constants.Operaciones.UPDATE;

                context.Threads.Update(thread);

                await context.SaveChangesAsync();

                return thread;
            }
        }

        public async Task<Entities.Thread> UpdateThreadStatusAndCreateMessage(decimal threadId, string threadStatus, Entities.Message newMessage, string user)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                var thread = await context.Threads.FindAsync(threadId);

                thread.Status = threadStatus;

                thread.UsuarioUltimaModif = user;
                thread.FechaUltimaModif = DateTime.Now;
                thread.AccionUltimaModif = Constants.Operaciones.UPDATE;

                context.Threads.Update(thread);

                context.Messages.Add(newMessage);

                await context.SaveChangesAsync();

                return thread;
            }
        }

        public async Task<Entities.Thread> UpdateThreadAndCreateMessageAndCreateRun(decimal threadId, string threadStatus, string threadDescription, string idThread, Message newMessage, Run newRun, string user)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                var thread = await context.Threads.FindAsync(threadId);

                thread.Status = threadStatus;
                thread.IdThread = idThread;
                thread.Description = threadDescription;

                thread.PromptTokens += newRun.PromptTokens;
                thread.CompletionTokens += newRun.CompletionTokens;
                thread.TotalTokens += newRun.TotalTokens;

                thread.UsuarioUltimaModif = user;
                thread.FechaUltimaModif = DateTime.Now;
                thread.AccionUltimaModif = Constants.Operaciones.UPDATE;

                context.Threads.Update(thread);
                context.Runs.Add(newRun);
                context.Messages.Add(newMessage);

                await context.SaveChangesAsync();

                return thread;
            }
        }

        public async Task<Entities.Thread> UpdateThreadAndCreateMessageAndCreateRun(decimal threadId, string threadStatus, List<Message> newMessages, Run newRun, List<Attachment> newFiles, string user)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                var thread = await context.Threads.FindAsync(threadId);

                if (thread == null)
                {
                    throw new Exception("Thread not found");
                }

                thread.Status = threadStatus;

                if (thread.PromptTokens.HasValue && newRun.PromptTokens.HasValue)
                {
                    thread.PromptTokens += newRun.PromptTokens.Value;
                }

                if (thread.CompletionTokens.HasValue && newRun.CompletionTokens.HasValue)
                {
                    thread.CompletionTokens += newRun.CompletionTokens.Value;
                }

                if (thread.TotalTokens.HasValue && newRun.TotalTokens.HasValue)
                {
                    thread.TotalTokens += newRun.TotalTokens.Value;
                }

                thread.UsuarioUltimaModif = user;
                thread.FechaUltimaModif = DateTime.Now;
                thread.AccionUltimaModif = Constants.Operaciones.UPDATE;

                context.Threads.Update(thread);

                newRun.ThreadId = threadId;
                context.Runs.Add(newRun);

                foreach (var message in newMessages)
                {
                    message.ThreadId = threadId;
                    context.Messages.Add(message);
                }

                foreach (var attachment in newFiles)
                {
                    attachment.ThreadId = threadId;
                    context.Attachments.Add(attachment);
                }

                await context.SaveChangesAsync();

                return thread;
            }
        }

        public async Task<IEnumerable<Entities.Thread>> GetAllActiveByUsernameAndProvider(string username, string provider)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.OrderByDescending(t => t.FechaAlta).Where(t => t.User.Username.Equals(username) && t.Provider.Equals(provider)).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Entities.Thread>> GetAllActiveByUsername(string username)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.OrderByDescending(t => t.FechaAlta).Where(t => t.User.Username.Equals(username)).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Entities.Thread>> GetAllActiveWithUser()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Threads.OrderByDescending(t => t.FechaAlta).Include(t => t.User).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Entities.Thread>> GetAllThreadsBuscadorThreads(string username, string status, string provider)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var queryableThreads = _context.Threads.Include(t => t.User).AsQueryable();

                //Condiciones
                if (!string.IsNullOrEmpty(status))
                    queryableThreads = queryableThreads.Where(t => t.Status.Equals(status)).AsQueryable();
                if (!string.IsNullOrEmpty(provider))
                    queryableThreads = queryableThreads.Where(t => t.Provider.Equals(provider)).AsQueryable();
                if (!string.IsNullOrEmpty(username))
                    queryableThreads = queryableThreads.Where(t => t.User.NombreCompleto.Contains(username)).AsQueryable();

                return await queryableThreads.OrderByDescending(t => t.FechaAlta).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
