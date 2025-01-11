using IntegracaoCSV.Core.Entity;
using IntegracaoCSV.Infra.Repository;

namespace IntegracaoCSV.Core.UseCase
{
    public class IntegraFilmesIndicados
    {
        private readonly IIntegracaoCSVRepository _repository;

        public IntegraFilmesIndicados(IIntegracaoCSVRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(string arquivoTxt)
        {
            var conteudoLinhasArquivo = arquivoTxt.ToString().Split("\\n");

            var indiceColunas = conteudoLinhasArquivo[0].Split(";");

            (string, int)[] arrayColuna = new (string, int)[indiceColunas.Length];

            for (int i = 0; i < indiceColunas.Length; i++)
            {
                arrayColuna[i] = (indiceColunas[i], i);
            }

            for (int i = 1; i < conteudoLinhasArquivo.Length; i++)
            {
                var linha = conteudoLinhasArquivo[i].Trim();
                if (!string.IsNullOrEmpty(linha))
                {
                    var arrayLinha = linha.Split(";");
                    var indicado = new FilmesIndicados(arrayLinha[0], arrayLinha[1], arrayLinha[2], arrayLinha[3], arrayLinha[4] == "yes" ? 1 : 0);
                    await _repository.AdicionaFilmeIndicado(indicado);
                }
            }
        }
    }
}
