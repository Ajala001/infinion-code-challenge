using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Queries
{
    public record GetAllProductsQuery(int PageSize, int PageNumber) : IRequest<PagedResponse<IEnumerable<ProductResponseDto>>>;

    public class GetAllProductsQueryHandler(IProductService productService)
        : IRequestHandler<GetAllProductsQuery, PagedResponse<IEnumerable<ProductResponseDto>>>
    {
        public async Task<PagedResponse<IEnumerable<ProductResponseDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await productService.GetProductsAsync(request.PageSize, request.PageNumber);
        }
    }
}
