using Shared.OpenDataSig;

namespace OpenDataSigAPI.Services.OpenDataSig
{
    public interface IOpenDataSigService
    {
        Task<RespuestaMensajeOpenDataSig> ManageMessageUi(RequestMessageOpenDataSig message);
    }
}
