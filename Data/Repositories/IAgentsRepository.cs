using AtencionUsuarios.Data.Entities;

namespace AtencionUsuarios.Data.Repositories
{
    public interface IAgentsRepository : IBaseRepository<Agent>
    {
        Task<string> GetInstructionsById(decimal agentId);
    }
}
