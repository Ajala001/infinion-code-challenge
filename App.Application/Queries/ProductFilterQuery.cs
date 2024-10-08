using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Queries
{
    public record ProductFilterQuery(ProductFilterDto ProductFilter)
        : IRequest<PagedResponse<IEnumerable<ProductResponseDto>>>;

    public class SearchProductQueryHandler(IProductService productService)
        : IRequestHandler<ProductFilterQuery, PagedResponse<IEnumerable<ProductResponseDto>>>
    {
        public async Task<PagedResponse<IEnumerable<ProductResponseDto>>> Handle(ProductFilterQuery request, CancellationToken cancellationToken)
        {
            return await productService.ProductFilterAsync(request.ProductFilter);
        }
    }
}
