using Shared.Functions;

namespace Services.Functions.Desfibriladores
{
    public interface IDesfibriladoresService
    {
        Task<DesfibriladoresResponse> GetDesfibriladoresAsync();
        string ParseData(DesfibriladoresResponse response);
    }
}
