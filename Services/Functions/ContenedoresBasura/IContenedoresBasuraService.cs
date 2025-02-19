using Shared.Functions;

namespace Services.Functions.ContenedoresBasura
{
    public interface IContenedoresBasuraService
    {
        Task<ContenedoresBasuraResponse> GetContenedoresBasuraAsync();

        string ParseData(ContenedoresBasuraResponse response);
    }
}
