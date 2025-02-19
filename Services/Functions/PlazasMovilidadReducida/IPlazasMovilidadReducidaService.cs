using Shared.Functions;

namespace Services.Functions.PlazasMovilidadReducida
{
    public interface IPlazasMovilidadReducidaService
    {
        Task<PlazasMovilidadReducidaResponse> GetPlazasMovilidadReducidaAsync();
        string ParseData(PlazasMovilidadReducidaResponse response);
    }
}
