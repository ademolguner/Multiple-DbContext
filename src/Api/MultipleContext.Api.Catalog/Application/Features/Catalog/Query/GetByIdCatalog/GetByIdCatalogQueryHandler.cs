using MediatR;
using MultipleContext.Api.Catalog.Application.Models.Dto;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Interfaces.Queryable;

namespace MultipleContext.Api.Catalog.Application.Features.Catalog.Query.GetByIdCatalog;

public class GetByIdCatalogQueryHandler:IRequestHandler<GetByIdCatalogQuery,CatalogDto>
{
     
    private readonly IQueryableCatalogRepository _queryableCatalogRepository;
    public GetByIdCatalogQueryHandler(IQueryableCatalogRepository queryableCatalogRepository)
    {
        _queryableCatalogRepository = queryableCatalogRepository ?? throw new ArgumentNullException(nameof(queryableCatalogRepository));
    }

    public async Task<CatalogDto> Handle(GetByIdCatalogQuery request, CancellationToken cancellationToken)
    {
        var model=await _queryableCatalogRepository.GetByIdAsync(id:request.Id);
        if (model is null)
            throw new ArgumentNullException(nameof(Domain.Entities.Catalog.Id));

        return new CatalogDto
        {
            Id = model.Id,
            Name = model.Name,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            IsDeleted = false
        };
    }
}