using OpenDataSigAPI.Data.Context;

namespace OpenDataSigAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OpenDataSigAPIContext _context;

        public UnitOfWork(OpenDataSigAPIContext context, IAgentsRepository agentsRepository,
                          IUsersRepository usersRepository, IThreadsRepository threadsRepository,
                          IRunsRepository runsRepository, IMessagesRepository messagesRepository,
                          ILogsRepository logsRepository, IAttachmentsRepository attachmentsRepository,
                          IFilesRepository filesRepository)
        {
            _context = context;
            Agents = agentsRepository;
            Users = usersRepository;
            Threads = threadsRepository;
            Runs = runsRepository;
            Messages = messagesRepository;
            Logs = logsRepository;
            Attachments = attachmentsRepository;
            Files = filesRepository;
        }

        public IAgentsRepository Agents { get; }
        public IUsersRepository Users { get; }
        public IThreadsRepository Threads { get; }
        public IRunsRepository Runs { get; }
        public IMessagesRepository Messages { get; }
        public ILogsRepository Logs { get; }
        public IAttachmentsRepository Attachments { get; }
        public IFilesRepository Files { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
