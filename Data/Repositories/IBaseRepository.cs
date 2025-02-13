using Microsoft.EntityFrameworkCore;

namespace AtencionUsuarios.Data.Repositories
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Permite crear una entidad de un tipo dado
        /// </summary>
        /// <param name="entity">Entidad que se creará</param>
        /// <param name="user">Usuario que realiza la operación</param>
        /// <returns>No tiene valor de retorno</returns>
        Task Create(T entity, string user);
        /// <summary>
        /// Devuelve todos los elementos de un tipo dado, tanto activos como inactivos. Devuelve también todos los elementos de navegación relacionados.
        /// </summary>
        /// <returns>Listado del tipo dado</returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Devuelve todos los elementos activos de un tipo dado. Devuelve también todos los elementos de navegación relacionados que estén activos.
        /// </summary>
        /// <returns>Listado del tipo dado</returns>
        Task<IEnumerable<T>> GetAllActive();
        /// <summary>
        /// Devuelve todos los elementos inactivos de un tipo dado. No devuelve ningún elemento de navegación relacionado.
        /// </summary>
        /// <returns>Listado del tipo dado</returns>
        Task<IEnumerable<T>> GetAllInactive();
        /// <summary>
        /// Devuelve los datos de una entidad según su id. Devuelve también todos los elementos de navegación relacionados,tanto activos como inactivos.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>La entidad del tipo dado</returns>
        Task<T> GetById(decimal id);
        /// <summary>
        /// Permite actualizar la entidad dada
        /// </summary>
        /// <param name="entity">Entidad a actualizar con los nuevos datos</param>
        /// <param name="id">id de la entidad a actualizar</param>
        /// <param name="user">Usuario que realiza la operación</param>
        /// <returns>No tiene valor de retorno</returns>
        Task Update(T entity,decimal id, string user);
        /// <summary>
        /// Permite borrar la entidad dada
        /// </summary>
        /// <param name="id">id de la entidad a borrar</param>
        /// <returns>No tiene valor de retorno</returns>
        Task Delete(decimal id);
        /// <summary>
        /// Permite realizar un borrado lógico la entidad dada
        /// </summary>
        /// <param name="id">id de la entidad a borrar</param>
        /// <param name="user">Usuario que realiza la operación</param>
        /// <returns>No tiene valor de retorno</returns>
        Task DeleteLogical(decimal id, string motivoBaja, string user);
        /// <summary>
        /// Permite reactivar la entidad dada. Elimina la fecha y el motivo de baja
        /// </summary>
        /// <param name="id">id de la entidad a borrar</param>
        /// <param name="user">Usuario que realiza la operación</param>
        /// <returns>La entidad reactivada</returns>
        Task<T> Reactivate(decimal id, string user);
    }
}
