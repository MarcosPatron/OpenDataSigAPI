using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request;
using OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response;
using OpenDataSigAPI.Shared.Models.OpenAi.Chat.Request;
using OpenDataSigAPI.Shared.Models.OpenAi.Chat.Response;
using OpenDataSigAPI.Shared.Models.OpenAi.Files.Request;
using Shared.Models.OpenAi.Assistant.Request;
using Shared.Models.OpenAi.Assistant.Response;

namespace Services.OpenAi
{
    public interface IOpenAiService
    {
        //ASISSTANT API
        Task<Message> CreateMessageAsync(CreateMessage request, string threadId);
        Task<Run> CreateRunAsync(CreateRun request, string threadId);
        Task<Run> CreateThreadAndRunAsync(CreateThreadAndRun request);
        Task<OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response.Thread> CreateThreadAsync(CreateThread request);
        Task<ListMessage> ListMessageAsync(string threadId, int? limit = null, string? order = null, string? after = null, string? before = null);
        Task<Message> RetrieveMessageAsync(string threadId, string messageId);
        Task<Run> RetrieveRunAsync(string threadId, string runId);
        Task<OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response.Thread> RetrieveTheadAsync(string threadId);
        Task<Run> SubmitToolOutputsAsync(OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Request.SubmitToolOutputs request, string threadId, string runId);

        //CHAT
        Task<ChatCompletionResponse> ChatCompletion(ChatCompletionRequest request);

        //FILES
        Task<OpenDataSigAPI.Shared.Models.OpenAi.Assistant.Response.File> UploadFile(UploadFile request);
        Task DeleteFile(string fileId);

        //VECTOR STORES
        Task<VectorStore> CreateVectorStore(CreateVectorStore request);
        Task DeleteVectorStore(string vectorStoreId);

        //VECTOR STORES
        Task<VectorStoreFile> CreateVectorStoreFile(CreateVectorStoreFile request, string vectorStoreId);
        Task DeleteVectorStoreFile(string vectorStoreId, string fileId);
    }
}