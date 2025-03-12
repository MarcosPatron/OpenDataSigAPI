using Microsoft.Extensions.Configuration;
using Shared.Functions;
using System.Text;
using System.Text.Json;

namespace Services.Functions.PlazasMovilidadReducida
{
    public class PlazasMovilidadReducidaService : IPlazasMovilidadReducidaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PlazasMovilidadReducidaService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<PlazasMovilidadReducidaResponse> GetPlazasMovilidadReducidaAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configuration["URL_APIs:PlazasMovilidadReducida"]);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                // Deserializar JSON con System.Text.Json
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<PlazasMovilidadReducidaResponse>(jsonResponse, options);
                return apiResponse ?? new PlazasMovilidadReducidaResponse();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new PlazasMovilidadReducidaResponse();
            }
        }

        public string ParseData(PlazasMovilidadReducidaResponse plazasMovilidadReducuda)
        {
            StringBuilder datos = new StringBuilder();

            // Encabezados
            datos.AppendLine("barrio,direccion,distrito,latitud,longitud");

            // Recorrer cada farmacia
            foreach (var feature in plazasMovilidadReducuda.FeatureList)
            {
                var properties = feature.Properties;

                datos.Append("\"").Append(properties.Barrio).Append("\"");
                datos.Append(",\"")
                     .Append(properties.TipoVia).Append(" ")
                     .Append(properties.NombreVia).Append(" ")
                     .Append(properties.NumPol).Append("\"");
                datos.Append(",\"").Append(properties.Distrito).Append("\"");
                datos.Append(",\"").Append(feature.Geometry.Coordinates[0][0]).Append("\"");
                datos.Append(",\"").Append(feature.Geometry.Coordinates[0][1]).Append("\"");
                datos.AppendLine();
            }

            Console.WriteLine(datos.ToString());

            return datos.ToString();
        }
    }
}
