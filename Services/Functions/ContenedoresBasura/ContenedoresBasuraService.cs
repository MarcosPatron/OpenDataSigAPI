using Microsoft.Extensions.Configuration;
using Shared.Functions;
using System.Text;
using System.Text.Json;

using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

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
                HttpResponseMessage response = await _httpClient.GetAsync(_configuration["URL_APIs:ContenedoresBasura"]);
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
            datos.AppendLine("tipo,estado,latitud,longitud");

            // Definir sistema de coordenadas de origen (ETRS89 UTM Zone 29N)
            var sourceCS = ProjectedCoordinateSystem.WGS84_UTM(29, true);

            // Definir sistema de coordenadas de destino (WGS84 Lat/Lon)
            var targetCS = GeographicCoordinateSystem.WGS84;

            // Crear transformador de coordenadas
            var transformFactory = new CoordinateTransformationFactory();
            var transformation = transformFactory.CreateFromCoordinateSystems(sourceCS, targetCS);



            // Recorrer cada contenedor
            foreach (var feature in response.FeaturesList)
            {

                var properties = feature.Properties;

                double x = feature.Geometry.Coordinates[0];
                double y = feature.Geometry.Coordinates[1]; 

                // Transformar coordenadas
                double[] result = transformation.MathTransform.Transform(new double[] { x, y });

                datos.Append(",\"").Append(properties.Tipo).Append("\"");
                datos.Append(",\"").Append(properties.Estado).Append("\"");
                datos.Append(",\"").Append(result[0]).Append("\"");
                datos.Append(",\"").Append(result[1]).Append("\"");
                datos.AppendLine();
            }

            return datos.ToString();
        }
    }
}
