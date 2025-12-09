using Microsoft.AspNetCore.Mvc;

namespace Loft.Code.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoftController : ControllerBase
{
    private readonly ILogger<LoftController> _logger;

    public LoftController(ILogger<LoftController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Task<IActionResult> Get()
    {
        return Task.FromResult<IActionResult>(Ok("Loft.Code.Api running"));
    }
}
