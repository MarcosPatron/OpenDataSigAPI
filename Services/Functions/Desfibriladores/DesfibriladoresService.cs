using Microsoft.Extensions.Configuration;
using Shared.Functions;
using System.Text;
using System.Text.Json;

namespace Services.Functions.Desfibriladores
{
    public class DesfibriladoresService : IDesfibriladoresService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DesfibriladoresService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<DesfibriladoresResponse> GetDesfibriladoresAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configuration["URL_APIs:Desfibriladores"]);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                // Deserializar JSON con System.Text.Json
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = JsonSerializer.Deserialize<DesfibriladoresResponse>(jsonResponse, options);
                return apiResponse ?? new DesfibriladoresResponse();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new DesfibriladoresResponse();
            }
        }

        public string ParseData(DesfibriladoresResponse desfibriladores)
        {
            StringBuilder datos = new StringBuilder();
            // Encabezados
            datos.AppendLine("tipo,situacion,descripcion,estado,latitud, longitud");
            // Recorrer cada desfibrilador
            foreach (var feature in desfibriladores.Features)
            {
                var properties = feature.Properties;
                datos.Append("\"").Append(feature.Type).Append("\"");
                datos.Append(",\"").Append(properties.Situacion).Append("\"");
                datos.Append(",\"").Append(properties.Descripcion).Append("\"");
                datos.Append(",\"").Append(properties.Direccion).Append("\"");
                datos.Append(",\"").Append(properties.Latitud).Append("\"");
                datos.Append(",\"").Append(properties.Longitud).Append("\"");
                datos.AppendLine();
            }

            Console.WriteLine(datos.ToString());

            return datos.ToString();
        }
    }
}
