using Shared.OpenDataSig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataSigAPI.Services.OpenDataSig
{
    public interface IOpenDataSigService
    {
        Task<RespuestaMensajeOpenDataSig> ManageMessageUi(string message, string? threadId);
    }
}
