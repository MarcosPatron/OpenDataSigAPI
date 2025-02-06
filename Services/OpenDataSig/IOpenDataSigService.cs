using Shared.OpenDataSig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OpenDataSig
{
    public interface IOpenDataSigService
    {
        public Task<RespuestaMensajeOpenDataSig> ManageMessageUi(string message, string? threadId);
    }
}
