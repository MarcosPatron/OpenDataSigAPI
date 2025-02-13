using AtencionUsuarios.Data.Entities;

namespace AtencionUsuarios.Data.Repositories
{
    public interface IAttachmentsRepository : IBaseRepository<Attachment>
    {
        Task<List<Attachment>> GetAllActiveByThreadId(decimal threadId);
    }
}
