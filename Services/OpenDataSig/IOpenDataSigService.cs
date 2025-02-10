using Shared.OpenDataSig;

namespace OpenDataSigAPI.Services.OpenDataSig
{
    public interface IOpenDataSigService
    {
        Task<RespuestaMensajeOpenDataSig> ManageMessageUi(string message, string? threadId);
    }
}
