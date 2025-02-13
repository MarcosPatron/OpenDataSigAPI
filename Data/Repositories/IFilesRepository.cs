using File = AtencionUsuarios.Data.Entities.File;

namespace AtencionUsuarios.Data.Repositories
{
    public interface IFilesRepository : IBaseRepository<File>
    {
        Task<List<File>> GetAllActiveByAgentId(decimal agentId);
    }
}