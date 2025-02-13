

using OpenDataSigAPI.Data.Entities;

namespace OpenDataSigAPI.Data.Repositories
{
    public interface IThreadsRepository : IBaseRepository<OpenDataSigAPI.Data.Entities.Thread>
    {
        Task<IEnumerable<Entities.Thread>> GetAllActiveWithUser();
        Task<IEnumerable<Entities.Thread>> GetAllThreadsBuscadorThreads(string username,string status, string provider);
        Task<IEnumerable<Entities.Thread>> GetAllActiveByUsername(string username);

        Task<IEnumerable<Entities.Thread>> GetAllActiveByUsernameAndProvider(string username,string provider);
        Task<Entities.Thread> GetThreadWithMessagesAndAttachmentsById(decimal id);
        Task<Entities.Thread> GetThreadWithMessagesAndAttachmentsAndUserById(decimal id);
        Task<OpenDataSigAPI.Data.Entities.Thread> GetThreadWithAttachmentsById(decimal id);
        Task<string> GetIdThreadByThreadId(decimal threadId);
        Task<Entities.Thread> UpdateThreadStatusAndId(decimal threadId, string threadStatus ,string IdThread, string user);
        Task<Entities.Thread> UpdateThreadStatusAndCreateMessage(decimal threadId, string threadStatus, Message newMessage, string user);
        Task<Entities.Thread> UpdateThreadAndCreateMessageAndCreateRun(decimal threadId, string threadStatus,string threadDescription,string idThread, Message newMessage, Run NewRun, string user);
        Task<Entities.Thread> UpdateThreadAndCreateMessageAndCreateRun(decimal threadId, string threadStatus, List<Message> newMessages, Run NewRun,List<Attachment> newFiles, string user);
    }
}
