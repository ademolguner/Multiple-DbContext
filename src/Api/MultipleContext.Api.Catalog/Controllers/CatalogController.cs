using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultipleContext.Api.Catalog.Application.Features.Catalog.Query.GetByIdCatalog;
using MultipleContext.Api.Catalog.Features.Catalog.Command.CreateCatalog;

namespace MultipleContext.Api.Catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var item = await _mediator.Send(new GetByIdCatalogQuery(id));
        return Ok(item);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCatalogCommand command)
    {
        var created = await _mediator.Send(command);
        return Ok(created);
    }
}