using System.Text;
using IntegracaoCSV.Core.Models;
using IntegracaoCSV.Core.UseCase;
using IntegracaoCSV.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoCSV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndicadosPiorFilmeController : ControllerBase
    {
        private readonly ILogger<IndicadosPiorFilmeController> _logger;

        public IndicadosPiorFilmeController(ILogger<IndicadosPiorFilmeController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/IntegracaoPorArquivo")]
        public async Task<IActionResult> PostIntegraFilmesIndicados([FromForm] PostIntegraArquivoRequest request,
            [FromServices] IntegraFilmesIndicados useCaseIntegraFilmesIndicados)
        {
            if (request.Arquivo.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi selecionado.");
            }

            var arquivoTxt = new StringBuilder();

            using (var stream = request.Arquivo.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var linha = "";
                    while ((linha = await reader.ReadLineAsync()) != null)
                    {
                        arquivoTxt.AppendLine(linha + "\\n");
                    }
                }
            }

            await useCaseIntegraFilmesIndicados.Execute(arquivoTxt.ToString());

            return Ok(new { request.Arquivo.FileName, Message = $"Arquivo recebido com sucesso! ${arquivoTxt}" });
        }

        [HttpGet]
        public async Task<FilmesIndicadosResponse> GetFilmesIndicados([FromServices] RetornaFilmesIndicados useCaseRetornaFilmesIndicados)
        {
            return await useCaseRetornaFilmesIndicados.Execute();
        }
    }
}
