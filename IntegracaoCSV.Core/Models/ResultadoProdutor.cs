using System.Text.Json.Serialization;

namespace IntegracaoCSV.Core.Models
{
    public class ResultadoProdutor
    {
        [JsonPropertyName("producer")]
        public string Produtor { get; set; }

        [JsonPropertyName("interval")]
        public int Intervalo { get; set; }

        [JsonPropertyName("previousWin")]
        public int AnoVitoriaAnterior { get; set; }

        [JsonPropertyName("followingWin")]
        public int AnoVitoriaSeguinte { get; set; }
    }
}
