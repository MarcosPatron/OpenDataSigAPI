
using Shared.Functions;

namespace Services.Functions.Farmacias
{
    public interface IFarmaciasService
    {
        Task<FarmaciasResponse> GetFarmaciasAsync();

        Task<string> GetDatosAsync();

        string ParseData(FarmaciasResponse response);
    }
}
