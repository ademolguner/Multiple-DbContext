using MediatR;
using MultipleContext.Api.Product.Application.Models.Dto;
using MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Queryable;

namespace MultipleContext.Api.Product.Application.Features.Product.Query.GetByIdProduct;

public class GetByIdProductQueryHandler:IRequestHandler<GetByIdProductQuery,ProductDto>
{
     
    private readonly IQueryableProductRepository _queryableProductRepository;
    public GetByIdProductQueryHandler(IQueryableProductRepository queryableProductRepository)
    {
        _queryableProductRepository = queryableProductRepository ?? throw new ArgumentNullException(nameof(queryableProductRepository));
    }

    public async Task<ProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var model=await _queryableProductRepository.GetByIdAsync(id:request.Id);
        if (model is null)
            throw new ArgumentNullException(nameof(Domain.Entities.Product.Id));

        return new ProductDto
        {
            Id = model.Id,
            Name = model.Name,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            IsDeleted = false
        };
    }
}