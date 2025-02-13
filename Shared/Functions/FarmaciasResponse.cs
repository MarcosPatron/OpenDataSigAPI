using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Functions
{

    public class FarmaciasResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("features")]
        public List<Feature> FeatureList { get; set; }
    }
    public class Feature
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("geometry_name")]
        public string GeometryName { get; set; }

        [JsonPropertyName("properties")]
        public PropertiesP Properties { get; set; }

        [JsonPropertyName("bbox")]
        public List<double> Bbox { get; set; }
    }
    public class Geometry
    {
        public string Type { get; set; }

        public List<List<double>> Coordinates { get; set; }
    }

    public class PropertiesP
    {
        [JsonPropertyName("nombretitu")]
        public string NombreTitu { get; set; }

        [JsonPropertyName("tipovia")]
        public string TipoVia { get; set; }

        [JsonPropertyName("codigovia")]
        public int CodigoVia { get; set; }

        [JsonPropertyName("nombrevia")]
        public string NombreVia { get; set; }

        [JsonPropertyName("numpol")]
        public string NumPol { get; set; }

        [JsonPropertyName("nucleo")]
        public string Nucleo { get; set; }

        [JsonPropertyName("cpostal")]
        public int CPostal { get; set; }

        [JsonPropertyName("horario")]
        public string Horario { get; set; }

        [JsonPropertyName("horariom")]
        public string HorarioM { get; set; }

        [JsonPropertyName("horariot_i")]
        public string HorarioTI { get; set; }

        [JsonPropertyName("horariot_v")]
        public string HorarioTV { get; set; }

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; }

        [JsonPropertyName("fax")]
        public string Fax { get; set; }

        [JsonPropertyName("tipo_horar")]
        public string TipoHorar { get; set; }

        [JsonPropertyName("urgencias")]
        public string Urgencias { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("coord_x")]
        public int CoordX { get; set; }

        [JsonPropertyName("coord_y")]
        public int CoordY { get; set; }

        [JsonPropertyName("wgs84_x")]
        public double? Wgs84X { get; set; }

        [JsonPropertyName("wgs84_y")]
        public double? Wgs84Y { get; set; }
    }
}
