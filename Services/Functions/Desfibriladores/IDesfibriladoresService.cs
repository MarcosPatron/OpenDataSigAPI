using Shared.Functions;

namespace Services.Functions.Desfibriladores
{
    interface IDesfibriladoresService
    {
        Task<DesfibriladoresResponse> GetDesfibriladoresAsync();
        string ParseData(DesfibriladoresResponse response);
    }
}
