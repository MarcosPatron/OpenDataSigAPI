using OpenDataSigAPI.Data.Entities;

namespace OpenDataSigAPI.Data.Repositories
{
    public interface ILogsRepository : IBaseRepository<Log>
    {
        Task LogFatal(string objeto, string metodo, string message, string stackTrace, Exception innerException, string user);
        Task LogError(string objeto, string metodo, string message, string descripcion);
        Task LogInfo(string objeto, string metodo, string message, string descripcion, string user);
        Task LogWarning(string objeto, string metodo, string message, string descripcion, string user);
    }
}
