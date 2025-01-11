using System.Text.Json.Serialization;

namespace IntegracaoCSV.Core.Models
{
    public class FilmesIndicadosResponse
    {
        public FilmesIndicadosResponse()
        {
            
        }
        public FilmesIndicadosResponse(ResultadoProdutor produtorMaiorIntervalo, ResultadoProdutor produtorDoisPremiosMaisRapido)
        {
            ProdutorMaiorIntervalo = produtorMaiorIntervalo;
            ProdutorDoisPremiosMaisRapido = produtorDoisPremiosMaisRapido;
        }

        [JsonPropertyName("producer_longest_interval")]
        public ResultadoProdutor ProdutorMaiorIntervalo { get; set; }

        [JsonPropertyName("producer_two_awards_fastest")]
        public ResultadoProdutor ProdutorDoisPremiosMaisRapido { get; set; }
    }
}
