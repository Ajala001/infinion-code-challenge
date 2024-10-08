using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Commands.Product
{
    public record CreateProductCommand(CreateProductDto Product) : IRequest<ApiResponse<ProductResponseDto>>;

    public class CreateProductCommandHandler(IProductService productService)
        : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponseDto>>
    {
        public async Task<ApiResponse<ProductResponseDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await productService.CreateAsync(request.Product);
        }
    }
}
