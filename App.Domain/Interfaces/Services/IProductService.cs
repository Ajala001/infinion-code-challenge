using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;

namespace App.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponseDto>> CreateAsync(CreateProductDto request);
        Task<ApiResponse<ProductResponseDto>> UpdateAsync(Guid productId, UpdateProductDto request);
        Task<ApiResponse<ProductResponseDto>> DeleteAsync(Guid productId);
        Task<PagedResponse<IEnumerable<ProductResponseDto>>> GetProductsAsync(int pageSize, int pageNumber);
        Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(Guid productId);
        Task<PagedResponse<IEnumerable<ProductResponseDto>>> ProductFilterAsync(ProductFilterDto request);
    }
}
