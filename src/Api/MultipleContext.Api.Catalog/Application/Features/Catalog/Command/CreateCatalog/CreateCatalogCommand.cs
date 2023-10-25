using MediatR;
using MultipleContext.Api.Catalog.Application.Models.Dto;

namespace MultipleContext.Api.Catalog.Features.Catalog.Command.CreateCatalog;

public class CreateCatalogCommand:IRequest<CatalogDto>
{
    public CreateCatalogCommand(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}