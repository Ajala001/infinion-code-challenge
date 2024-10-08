using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Commands.Product
{
    public record DeleteProductCommand(Guid ProductId) : IRequest<ApiResponse<ProductResponseDto>>;

    public class DeleteProductCommandHandler(IProductService productService)
        : IRequestHandler<DeleteProductCommand, ApiResponse<ProductResponseDto>>
    {
        public async Task<ApiResponse<ProductResponseDto>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await productService.DeleteAsync(request.ProductId);
        }
    }
}
