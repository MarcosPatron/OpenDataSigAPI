using System.Text.Json.Serialization;

namespace Shared.Functions
{
    public class PuntosLimpiosResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("features")]
        public List<FeaturePL> FeatureList { get; set; }
    }

    public class FeaturePL
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("geometry")]
        public GeometryPL Geometry { get; set; }

        [JsonPropertyName("geometry_name")]
        public string GeometryName { get; set; }

        [JsonPropertyName("properties")]
        public PropertiesPL Properties { get; set; }

        [JsonPropertyName("bbox")]
        public List<double> Bbox { get; set; }
    }

    public class GeometryPL
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class PropertiesPL
    {
        [JsonPropertyName("mslink")]
        public int Mslink { get; set; }

        [JsonPropertyName("mapid")]
        public int Mapid { get; set; }

        [JsonPropertyName("identifica")]
        public int Identifica { get; set; }

        [JsonPropertyName("codigovia")]
        public string CodigoVia { get; set; }

        [JsonPropertyName("tipovia")]
        public string TipoVia { get; set; }

        [JsonPropertyName("nombrevia")]
        public string NombreVia { get; set; }

        [JsonPropertyName("numpol")]
        public string NumPol { get; set; }

        [JsonPropertyName("descripcio")]
        public string Descripcion { get; set; }

        [JsonPropertyName("horario")]
        public string Horario { get; set; }

        [JsonPropertyName("wgs84_x")]
        public double Latitud { get; set; }

        [JsonPropertyName("wgs84_y")]
        public double Longitud { get; set; }

        [JsonPropertyName("id")]
        public object Id { get; set; }
    }
}