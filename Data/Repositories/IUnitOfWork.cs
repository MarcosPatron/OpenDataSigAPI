namespace AtencionUsuarios.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAgentsRepository Agents { get; }
        IUsersRepository Users { get; }
        IThreadsRepository Threads { get; }
        IRunsRepository Runs { get; }
        IMessagesRepository Messages { get; }
        ILogsRepository Logs { get; }
        IAttachmentsRepository Attachments { get; }
        IFilesRepository Files { get; }

        Task<int> CompleteAsync();
    }
}
