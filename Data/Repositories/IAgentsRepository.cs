using OpenDataSigAPI.Data.Entities;

namespace OpenDataSigAPI.Data.Repositories
{
    public interface IAgentsRepository : IBaseRepository<Agent>
    {
        Task<string> GetInstructionsById(decimal agentId);
    }
}
