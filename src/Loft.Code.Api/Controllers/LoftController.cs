using Loft.Code.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Loft.Code.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoftController : ControllerBase
{
    private readonly ILogger<LoftController> _logger;
    private readonly ICalculadoraAluguelService _calculadoraAluguel;

    public LoftController(ILogger<LoftController> logger, ICalculadoraAluguelService calculadoraAluguel)
    {
        _logger = logger;
        _calculadoraAluguel = calculadoraAluguel;
    }

    [HttpGet]
    public async Task<IActionResult> CalculaPercentual()
    {
        var listaImoveis = new List<CalculaPercentualImovelRequestDto>
        {
            new CalculaPercentualImovelRequestDto
            {
                Id = 1,
                TipoImovel = "apartamento",
                TipoContrato = "reajustado",
                ValorBase = 1200,
                PercentualReajuste = 0.1M,
            },
            new CalculaPercentualImovelRequestDto
            {
                Id = 2,
                TipoImovel = "casa",
                TipoContrato = "fixo",
                ValorBase = 1500,
            }
        };

        var result = _calculadoraAluguel.CalcularAluguel(listaImoveis);
        return Ok(result);

    }
}
