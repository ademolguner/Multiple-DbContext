using MediatR;
using MultipleContext.Api.Catalog.Application.Models.Dto;

namespace MultipleContext.Api.Catalog.Application.Features.Catalog.Query.GetByIdCatalog;

public class GetByIdCatalogQuery:IRequest<CatalogDto>
{
    public GetByIdCatalogQuery(string id)
    {
        Id = id;
    }
    public  string Id { get; set; }
}