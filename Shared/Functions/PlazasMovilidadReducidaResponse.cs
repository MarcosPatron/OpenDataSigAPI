using System.Text.Json.Serialization;

namespace Shared.Functions
{
    public class PlazasMovilidadReducidaResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("features")]
        public List<FeaturePMR> FeatureList { get; set; }
    }

    public class FeaturePMR
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("geometry")]
        public GeometryPMR Geometry { get; set; }

        [JsonPropertyName("geometry_name")]
        public string GeometryName { get; set; }

        [JsonPropertyName("properties")]
        public PropertiesPMR Properties { get; set; }

        [JsonPropertyName("bbox")]
        public List<double> Bbox { get; set; }
    }

    public class GeometryPMR
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<List<double>> Coordinates { get; set; }
    }

    public class PropertiesPMR
    {
        [JsonPropertyName("wgs84_x")]
        public double Wgs84X { get; set; }

        [JsonPropertyName("wgs84_y")]
        public double Wgs84Y { get; set; }

        [JsonPropertyName("codigo_via")]
        public string CodigoVia { get; set; }

        [JsonPropertyName("coord_x")]
        public int CoordX { get; set; }

        [JsonPropertyName("coord_y")]
        public int CoordY { get; set; }

        [JsonPropertyName("enlacefoto")]
        public string EnlaceFoto { get; set; }

        [JsonPropertyName("f_creacion")]
        public string FCreacion { get; set; }

        [JsonPropertyName("f_repintad")]
        public string FRepintad { get; set; }

        [JsonPropertyName("gestion")]
        public string Gestion { get; set; }

        [JsonPropertyName("matricula")]
        public string Matricula { get; set; }

        [JsonPropertyName("nombre_via")]
        public string NombreVia { get; set; }

        [JsonPropertyName("nucleo")]
        public string Nucleo { get; set; }

        [JsonPropertyName("numpol")]
        public string NumPol { get; set; }

        [JsonPropertyName("tipo_via")]
        public string TipoVia { get; set; }

        [JsonPropertyName("url_ficha")]
        public string UrlFicha { get; set; }

        [JsonPropertyName("url_pdf")]
        public string UrlPdf { get; set; }

        [JsonPropertyName("distrito")]
        public string Distrito { get; set; }

        [JsonPropertyName("barrio")]
        public string Barrio { get; set; }

        [JsonPropertyName("codbarrio")]
        public int CodBarrio { get; set; }

        [JsonPropertyName("url_via")]
        public string UrlVia { get; set; }

        [JsonPropertyName("publica")]
        public string Publica { get; set; }

        [JsonPropertyName("url_foto")]
        public string UrlFoto { get; set; }

        [JsonPropertyName("mslink")]
        public int MsLink { get; set; }
    }
}
