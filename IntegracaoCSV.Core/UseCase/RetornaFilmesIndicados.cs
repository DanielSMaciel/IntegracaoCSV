using IntegracaoCSV.Core.Entity;
using IntegracaoCSV.Core.Models;
using IntegracaoCSV.Infra.Repository;

namespace IntegracaoCSV.Core.UseCase
{
    public class RetornaFilmesIndicados
    {
        private readonly IIntegracaoCSVRepository _repository;

        public RetornaFilmesIndicados(IIntegracaoCSVRepository repository)
        {
            _repository = repository;
        }

        public async Task<FilmesIndicadosResponse> Execute()
        {
            var filmes = await _repository.RetornaFilmesIndicados();

            var produtorMaiorIntervalo = RetornaProdutorMaiorIntervaloPremiosConsecutivos(filmes);
            var produtorDoisPremiosMaisRapido = RetornaProdutorConseguiuDoisPremiosMaisRapido(filmes);

            return new FilmesIndicadosResponse(produtorMaiorIntervalo, produtorDoisPremiosMaisRapido);
        }

        public ResultadoProdutor RetornaProdutorMaiorIntervaloPremiosConsecutivos(List<FilmesIndicados> filmes)
        {
            return filmes.Where(x => x.Vencedor == 1)
                                  .GroupBy(x => x.Produtores)
                                  .Select(g =>
           {
               var anos = g
                   .OrderBy(f => int.Parse(f.Ano))
                   .Select(f => int.Parse(f.Ano))
                   .ToList();

               var maiorIntervalo = 0;
               var anoInicial = 0;
               var anoFinal = 0;

               for (int i = 1; i < anos.Count; i++)
               {
                   var intervalo = anos[i] - anos[i - 1];
                   if (intervalo > maiorIntervalo)
                   {
                       maiorIntervalo = intervalo;
                       anoInicial = anos[i - 1];
                       anoFinal = anos[i];
                   }
               }

               return new ResultadoProdutor
               {
                   Produtor = g.Key,
                   Intervalo = maiorIntervalo,
                   AnoVitoriaAnterior = anoInicial,
                   AnoVitoriaSeguinte = anoFinal
               };
           })
           .OrderByDescending(r => r.Intervalo)
           .FirstOrDefault();
        }

        public ResultadoProdutor RetornaProdutorConseguiuDoisPremiosMaisRapido(List<FilmesIndicados> filmes)
        {
            return filmes
            .Where(f => f.Vencedor == 1) 
            .GroupBy(f => f.Produtores) 
            .Select(grupo =>
            {
                
                var anos = grupo
                    .OrderBy(f => int.Parse(f.Ano))
                    .Select(f => int.Parse(f.Ano))
                    .ToList();

                var menorIntervalo = int.MaxValue;
                var anoInicial = 0;
                var anoFinal = 0;

                for (int i = 1; i < anos.Count; i++)
                {
                    var intervalo = anos[i] - anos[i - 1];
                    if (intervalo < menorIntervalo)
                    {
                        menorIntervalo = intervalo;
                        anoInicial = anos[i - 1];
                        anoFinal = anos[i];
                    }
                }

                return new ResultadoProdutor
                {
                    Produtor = grupo.Key,
                    Intervalo = menorIntervalo,
                    AnoVitoriaAnterior = anoInicial,
                    AnoVitoriaSeguinte = anoFinal
                };
            })
            .Where(r => r.Intervalo < int.MaxValue) 
            .OrderBy(r => r.Intervalo)
            .FirstOrDefault();
        }
    }
}
