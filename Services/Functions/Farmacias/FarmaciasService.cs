﻿using Microsoft.Extensions.Configuration;
using Shared.Functions;
using System.Text;
using System.Text.Json;

namespace Services.Functions.Farmacias
{
    public class FarmaciasService : IFarmaciasService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FarmaciasService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<FarmaciasResponse> GetFarmaciasAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_configuration["URL_APIs:Farmacias"]);
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

        public string ParseData(FarmaciasResponse farmacias)
        {

            StringBuilder datos = new StringBuilder();

            // Encabezados
            datos.AppendLine("nombre,direccion,horario,telefono,latitud,longitud");

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
                datos.Append(",\"").Append(feature.Geometry.Coordinates[0][0]).Append("\"");
                datos.Append(",\"").Append(feature.Geometry.Coordinates[0][1]).Append("\"");
                datos.AppendLine();
            }

            return datos.ToString();
        }
    }

}
