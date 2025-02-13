using File = OpenDataSigAPI.Data.Entities.File;

namespace OpenDataSigAPI.Data.Repositories
{
    public interface IFilesRepository : IBaseRepository<File>
    {
        Task<List<File>> GetAllActiveByAgentId(decimal agentId);
    }
}