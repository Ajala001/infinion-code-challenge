using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Queries
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ApiResponse<ProductResponseDto>>;

    public class GetProductByIdQueryHandler(IProductService productService)
        : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponseDto>>
    {
        public async Task<ApiResponse<ProductResponseDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await productService.GetProductByIdAsync(request.ProductId);
        }
    }


}
