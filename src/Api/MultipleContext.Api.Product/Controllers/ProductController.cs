using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultipleContext.Api.Product.Application.Features.Product.Command.CreateProduct;
using MultipleContext.Api.Product.Application.Features.Product.Query.GetByIdProduct;

namespace MultipleContext.Api.Product.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var item = await _mediator.Send(new GetByIdProductQuery(id));
        return Ok(item);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductCommand command)
    {
        var created = await _mediator.Send(command);
        return Ok(created);
    }
}