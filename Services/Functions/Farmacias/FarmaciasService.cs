using Shared.Functions;
using System.Text;
using System.Text.Json;

namespace Services.Functions.Farmacias
{
    public class FarmaciasService : IFarmaciasService
    {
        private readonly string _baseUrl = "https://ide.caceres.es/geoserver/toponimia/";

        private readonly HttpClient _httpClient;

        public FarmaciasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FarmaciasResponse> GetFarmaciasAsync()
        {
            string url = $"{_baseUrl}ows?service=WFS&version=1.0.0&request=GetFeature&typeName=toponimia%3Afarmacias&maxFeatures=50&outputFormat=application%2Fjson";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserializar JSON con System.Text.Json
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var apiResponse = JsonSerializer.Deserialize<FarmaciasResponse>(jsonResponse, options);

                return apiResponse ?? new FarmaciasResponse();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new FarmaciasResponse();
            }
        }

        public async Task<string> GetDatosAsync()
        {
            // Implementación de ejemplo para GetDatosAsync
            return await Task.FromResult("Datos de ejemplo");
        }

        public string ParseData(FarmaciasResponse farmacias)
        {

            StringBuilder datos = new StringBuilder();

            // Encabezados
            datos.AppendLine("nombre,direccion,horario,telefono");

            // Recorrer cada farmacia
            foreach (var feature in farmacias.FeatureList)
            {
                var properties = feature.Properties;

                datos.Append("\"").Append(properties.NombreTitu).Append("\"");
                datos.Append(",\"")
                     .Append(properties.TipoVia).Append(" ")
                     .Append(properties.NombreVia).Append(" ")
                     .Append(properties.NumPol).Append("\"");
                datos.Append(",\"").Append(properties.Horario).Append("\"");
                datos.Append(",\"").Append(properties.Telefono).Append("\"");
                datos.AppendLine();
            }

            return datos.ToString();
        }
    }

}
