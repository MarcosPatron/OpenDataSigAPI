using OpenDataSigAPI.Data.Entities;

namespace OpenDataSigAPI.Data.Repositories
{
    public interface IAttachmentsRepository : IBaseRepository<Attachment>
    {
        Task<List<Attachment>> GetAllActiveByThreadId(decimal threadId);
    }
}
