using Microsoft.AspNetCore.Mvc;

namespace MultipleContext.Api.Catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ILogger<CatalogController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "catalogs")]
    public IEnumerable<string> Get() => new List<string>{"Come to back"};
}