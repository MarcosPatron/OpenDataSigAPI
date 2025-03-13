using Shared.Functions;

namespace Services.Functions.PuntoLimpio
{
    public interface IPuntosLimpiosService
    {
        Task<PuntosLimpiosResponse> GetPuntosLimpiosAsync();

        string ParseData(PuntosLimpiosResponse response);
    }
}
