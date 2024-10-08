using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Commands.Product
{
    public record UpdateProductCommand(Guid ProductId, UpdateProductDto Product) : IRequest<ApiResponse<ProductResponseDto>>;

    public class UpdateProductCommandHandler(IProductService productService)
        : IRequestHandler<UpdateProductCommand, ApiResponse<ProductResponseDto>>
    {
        public async Task<ApiResponse<ProductResponseDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await productService.UpdateAsync(request.ProductId, request.Product);
        }
    }
}
