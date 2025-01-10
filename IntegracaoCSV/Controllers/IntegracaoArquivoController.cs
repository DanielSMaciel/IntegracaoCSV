using System.Text;
using IntegracaoCSV.Core.UseCase;
using IntegracaoCSV.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegracaoCSV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegracaoArquivoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<IntegracaoArquivoController> _logger;

        public IntegracaoArquivoController(ILogger<IntegracaoArquivoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostIntegraArquivo([FromForm] PostIntegraArquivoRequest request, [FromServices] IntegraFilmesIndicados useCaseIntegraFilmesIndicados)
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
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
