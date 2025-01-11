using System.Text;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegracaoCSV.Test;
public class IndicadosPiorFilmeTest
{
    public class IntegracaoArquivoControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegracaoArquivoControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_DeveProcessarArquivoERetornarSucesso()
        {
            var fileContent = "Ano;Titulo;Estudio;Produtores;Vencedor\n" +
                              "1980;Filme A;Estudio X;Produtor 1;yes\n" +
                              "1985;Filme B;Estudio Y;Produtor 2;no\n" +
                              "1990;Filme C;Estudio Z;Produtor 3;yes";

            var content = new MultipartFormDataContent();
            var fileBytes = Encoding.UTF8.GetBytes(fileContent);
            content.Add(new ByteArrayContent(fileBytes), "Arquivo", "filmes.csv");

            var response = await _client.PostAsync("/IntegracaoPorArquivo", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("Arquivo recebido com sucesso", responseBody);
        }

        [Fact]
        public async Task Get_DeveRetornarFilmesIndicados()
        {
            var response = await _client.GetAsync("/IndicadosPiorFilme");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseBody);
        }
    }
}
