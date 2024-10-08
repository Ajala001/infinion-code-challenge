using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;

namespace App.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> SignInAsync(SignInDto request);
        Task<ApiResponse<UserResponseDto>> SignUpAsync(SignUpDto request);
        Task<ApiResponse<UserResponseDto>> ConfirmEmailAsync(string email, string token);
    }
}
