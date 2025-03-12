using System.Text.Json.Serialization;

namespace Shared.Functions
{
    public class ContenedoresBasuraResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("features")]
        public List<FeatureCB> FeaturesList { get; set; }
    }

    public class FeatureCB
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("geometry")]
        public GeometryCB Geometry { get; set; }

        [JsonPropertyName("geometry_name")]
        public string GeometryName { get; set; }

        [JsonPropertyName("properties")]
        public PropertiesCB Properties { get; set; }

        [JsonPropertyName("bbox")]
        public List<double> Bbox { get; set; }
    }

    public class GeometryCB
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class PropertiesCB
    {
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("id_punto_r")]
        public string IdPuntoR { get; set; }

        [JsonPropertyName("id_cont")]
        public string IdCont { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("ruta_asig")]
        public string RutaAsig { get; set; }

        [JsonPropertyName("dest_car")]
        public string DestCar { get; set; }

        [JsonPropertyName("fech_p_ser")]
        public string FechPSer { get; set; }

        [JsonPropertyName("frecu_lim")]
        public string FrecuLim { get; set; }

        [JsonPropertyName("f_ulti_lim")]
        public string FUltiLim { get; set; }

        [JsonPropertyName("fech_pro_l")]
        public string FechProL { get; set; }

        [JsonPropertyName("modelo_1")]
        public string Modelo1 { get; set; }

        [JsonPropertyName("fecha_dat")]
        public string FechaDat { get; set; }

        [JsonPropertyName("observ_pr")]
        public string ObservPr { get; set; }

        [JsonPropertyName("operador")]
        public string Operador { get; set; }

        [JsonPropertyName("barrio")]
        public string Barrio { get; set; }

        [JsonPropertyName("inciden")]
        public string Inciden { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

}
