using Microsoft.Extensions.Configuration;
using Shared.Functions;
using System.Text;
using System.Text.Json;

namespace Services.Functions.PuntoLimpio
{
    public class PuntosLimpiosService : IPuntosLimpiosService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public PuntosLimpiosService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<PuntosLimpiosResponse> GetPuntosLimpiosAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configuration["URL_APIs:PuntosLimpios"]);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                // Deserializar JSON con System.Text.Json
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<PuntosLimpiosResponse>(jsonResponse, options);
                return apiResponse ?? new PuntosLimpiosResponse();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new PuntosLimpiosResponse();
            }
        }
        public string ParseData(PuntosLimpiosResponse puntosLimpios)
        {
            StringBuilder datos = new StringBuilder();
            // Encabezados
            datos.AppendLine("direccion,descripcion,horario,latitud,longitud");
            // Recorrer cada punto limpio
            foreach (var feature in puntosLimpios.FeatureList)
            {
                var properties = feature.Properties;
                datos.Append(",\"")
                     .Append(properties.TipoVia).Append(" ")
                     .Append(properties.NombreVia).Append(" ")
                     .Append(properties.NumPol).Append("\"");
                datos.Append(",\"").Append(properties.Descripcion).Append("\"");
                datos.Append(",\"").Append(properties.Horario).Append("\"");
                datos.Append(",\"").Append(properties.Latitud).Append("\"");
                datos.Append(",\"").Append(properties.Longitud).Append("\"");
                datos.AppendLine();
            }

            return datos.ToString();
        }
    }
}
