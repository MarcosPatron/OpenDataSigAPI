
using Shared.Functions;

namespace Services.Functions.Farmacias
{
    public interface IFarmaciasService
    {
        Task<FarmaciasResponse> GetFarmaciasAsync();

        string ParseData(FarmaciasResponse response);
    }
}
