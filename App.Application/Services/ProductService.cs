using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace App.Application.Services
{
    public class ProductService(IProductRepository productRepository, IFileRepository fileRepository,
                                IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<ApiResponse<ProductResponseDto>> CreateAsync(CreateProductDto request)
        {
            var loginUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var existingProduct = await productRepository.GetProductAsync(pr => pr.Name == request.Name);
            if (existingProduct != null) return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = false,
                Message = $"{request.Name} product already exist"
            };

            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedAt = DateTime.Now,
                CreatedBy = loginUser!,
            };

            if (request.ImageUrl != null)
            {
                var imageUpload = await fileRepository.UploadAsync(request.ImageUrl);
                if (imageUpload != null)
                {
                    newProduct.ImageUrl = imageUpload;
                }
            }
            await productRepository.CreateAsync(newProduct);
            var result = await unitOfWork.SaveAsync();
            if(result > 0) return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = true,
                Message = "Product Created Successfully",
                Data = new ProductResponseDto
                {
                    Id = newProduct.Id,
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Price = newProduct.Price.ToString("C2", new System.Globalization.CultureInfo("en-NG"))
                }
            };

            return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = false,
                Message = "Something went Wrong",
            };


        }

        public async Task<ApiResponse<ProductResponseDto>> DeleteAsync(Guid productId)
        {
            var product = await productRepository.GetProductAsync(pr => pr.Id == productId);
            if (product == null) return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = false,
                Message = "Product not found"
            };

            productRepository.Delete(product);
            var result = await unitOfWork.SaveAsync();
            if(result > 0) return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = true,
                Message = "Product Created Successfully"
            };

            return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = false,
                Message = "Something went wrong"
            };
        }

        public async Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(Guid productId)
        {
            var product = await productRepository.GetProductAsync(pr => pr.Id == productId);
            if (product == null) return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = false,
                Message = "Product not found"
            };

            return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = true,
                Message = "Product Created Successfully",
                Data = new ProductResponseDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price.ToString("C2", new System.Globalization.CultureInfo("en-NG"))
                }
            };

        }

        public async Task<PagedResponse<IEnumerable<ProductResponseDto>>> GetProductsAsync(int pageSize, int pageNumber)
        {
            var products = await productRepository.GetProductsAsync();

            pageSize = pageSize > 0 ? pageSize : 5;
            pageNumber = pageNumber > 0 ? pageNumber : 1;

            if (products == null || !products.Any()) return new PagedResponse<IEnumerable<ProductResponseDto>>
            {
                IsSuccessful = false,
                Message = "Products not found"
            };

            var totalRecords = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // If pageNumber exceeds total pages, return an empty response
            if (pageNumber > totalPages)
            {
                return new PagedResponse<IEnumerable<ProductResponseDto>>
                {
                    IsSuccessful = true,
                    Message = "No more products available",
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    Data = new List<ProductResponseDto>()
                };
            }

            // Paginate the examinations
            var paginatedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginatedResponseData = paginatedProducts.Select(product => new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString("C2", new System.Globalization.CultureInfo("en-NG"))
            }).ToList();


            return new PagedResponse<IEnumerable<ProductResponseDto>>
            {
                IsSuccessful = true,
                Message = "Products Retrieved Successfuly",
                Data = paginatedResponseData
            };
        }

        public async Task<ApiResponse<ProductResponseDto>> UpdateAsync(Guid productId, UpdateProductDto request)
        {
            var product = await productRepository.GetProductAsync(pr => pr.Id == productId);
            if (product == null) return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = false,
                Message = "Product not found"
            };

            if (request.ImageUrl != null)
            {
                var imageUpload = await fileRepository.UploadAsync(request.ImageUrl);
                if (imageUpload != null)
                {
                    product.ImageUrl = imageUpload;
                }
            }

            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.Price = request.Price ?? product.Price;
            
            productRepository.Update(product);
            var result = await unitOfWork.SaveAsync();
            if(result > 0) return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = true,
                Message = "Product Created Successfully",
                Data = new ProductResponseDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price.ToString("C2", new System.Globalization.CultureInfo("en-NG"))
                }
            };

            return new ApiResponse<ProductResponseDto>
            {
                IsSuccessful = true,
                Message = "Something went wrong",
            };
        }

        public async Task<PagedResponse<IEnumerable<ProductResponseDto>>> ProductFilterAsync(ProductFilterDto request)
        {
            var products = await productRepository.GetProductsAsync();
            var searchedProducts = products.Where(product =>
                                !string.IsNullOrEmpty(request.SearchQuery) &&
                                (product.Name.Contains(request.SearchQuery, StringComparison.OrdinalIgnoreCase)
                                || decimal.TryParse(request.SearchQuery, out decimal price) && product.Price == price)
                            ).ToList();

            if (!searchedProducts.Any()) return new PagedResponse<IEnumerable<ProductResponseDto>>
            {
                IsSuccessful = false,
                Message = "No Match Found",
                Data = null
            };

            var totalRecords = searchedProducts.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize);

            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;
            var paginatedCourses = searchedProducts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            var responseData = paginatedCourses.Select(product => new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString("C2", new System.Globalization.CultureInfo("en-NG")),
            }).ToList();

            return new PagedResponse<IEnumerable<ProductResponseDto>>
            {
                IsSuccessful = true,
                Message = "Courses Retrieved Successfully",
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                Data = responseData
            };
        }

    }
}
