using Microsoft.EntityFrameworkCore;
using OpenDataSigAPI.Data.Context;
using OpenDataSigAPI.Data.Entities;
using Shared;

namespace OpenDataSigAPI.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbContextFactory<OpenDataSigAPIContext> _contextFactory;

        public UsersRepository(IDbContextFactory<OpenDataSigAPIContext> contextFactory)
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

        public async Task Create(User entity, string user)
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

                User entidad = await _context.Users.FindAsync(id);
                _context.Users.Remove(entidad);

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

                User entidad = await _context.Users.FindAsync(id);

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

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Users.IgnoreQueryFilters().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllActive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllInactive()
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();

                var allUser = await _context.Users.IgnoreQueryFilters().ToListAsync();
                var activeUser = await _context.Users.ToListAsync();

                return allUser.Except(activeUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetById(decimal id)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return await _context.Users.IgnoreQueryFilters().SingleOrDefaultAsync(u => u.Usersid == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> Reactivate(decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidad = await _context.Users.IgnoreQueryFilters().SingleOrDefaultAsync(u => u.Usersid == id);

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

        public async Task Update(User entity, decimal id, string user)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                var entidadAActualizar = await _context.Users.FindAsync(id);

                entidadAActualizar.Username = entity.Username;
                entidadAActualizar.NombreCompleto = entity.NombreCompleto;
                entidadAActualizar.Email = entity.Email;
                entidadAActualizar.Telefono = entity.Telefono;
                entidadAActualizar.Avatar = entity.Avatar;

                entidadAActualizar.UsuarioUltimaModif = user;
                entidadAActualizar.FechaUltimaModif = DateTime.Now;
                entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

                _context.Users.Update(entidadAActualizar);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsUser(string username)
        {
            try
            {
                var _context = (OpenDataSigAPIContext)CreateDbContext();
                return _context.Users.IgnoreQueryFilters().SingleOrDefault(u => u.Username.Equals(username)) != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetUserIdByUsername(string username)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                var user = context.Users.IgnoreQueryFilters().SingleOrDefault(u => u.Username.Equals(username));
                return user?.Usersid ?? 0;
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                return await context.Users.IgnoreQueryFilters().SingleOrDefaultAsync(u => u.Username.Equals(username));
            }
        }

        public async Task<User> UpdateAvatarAsyncByUsername(byte[] avatar, string username)
        {
            try
            {
                using (var _context = (OpenDataSigAPIContext)CreateDbContext())
                {
                    var entidadAActualizar = await _context.Users.IgnoreQueryFilters().SingleOrDefaultAsync(u => u.Username.Equals(username));

                    entidadAActualizar.Avatar = avatar;

                    entidadAActualizar.UsuarioUltimaModif = username;
                    entidadAActualizar.FechaUltimaModif = DateTime.Now;
                    entidadAActualizar.AccionUltimaModif = Constants.Operaciones.UPDATE;

                    _context.Users.Update(entidadAActualizar);
                    await _context.SaveChangesAsync();

                    return entidadAActualizar;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<byte[]> GetAvatarByUsername(string username)
        {
            using (var context = (OpenDataSigAPIContext)CreateDbContext())
            {
                return await context.Users.IgnoreQueryFilters().Where(u => u.Username.Equals(username)).Select(u => u.Avatar).SingleOrDefaultAsync();
            }
        }
    }
}
