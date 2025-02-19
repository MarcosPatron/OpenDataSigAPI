using Microsoft.Extensions.Configuration;
using Shared.Functions;
using System.Text;
using System.Text.Json;

namespace Services.Functions.ContenedoresBasura
{
    public class ContenedoresBasuraService : IContenedoresBasuraService // No se la he pasado al Asistente
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ContenedoresBasuraService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<ContenedoresBasuraResponse> GetContenedoresBasuraAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configuration["URL_API:ContenedoresBasura"]);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                // Deserializar JSON con System.Text.Json
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<ContenedoresBasuraResponse>(jsonResponse, options);
                return apiResponse ?? new ContenedoresBasuraResponse();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ContenedoresBasuraResponse();
            }
        }
        public string ParseData(ContenedoresBasuraResponse response)
        {
            StringBuilder datos = new StringBuilder();

            // Encabezados
            datos.AppendLine("nombre,direccion,horario,telefono,latitud,longitud");

            // Recorrer cada farmacia
            foreach (var feature in response.FeaturesList)
            {
                var properties = feature.Properties;

                datos.Append(",\"").Append(properties.Tipo).Append("\"");
                datos.Append(",\"").Append(properties.Estado).Append("\"");
                datos.Append(",\"").Append(feature.Geometry.Coordinates[0]).Append("\"");
                datos.Append(",\"").Append(feature.Geometry.Coordinates[1]).Append("\"");
                datos.AppendLine();
            }

            Console.WriteLine(datos.ToString());

            return datos.ToString();
        }
    }
}
