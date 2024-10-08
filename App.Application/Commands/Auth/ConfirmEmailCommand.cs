using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Commands.Auth
{
    public record ConfirmEmailCommand(string userEmail, string token) : IRequest<ApiResponse<UserResponseDto>>;

    public class ConfirmEmailCommandHandler(IAuthService authService)
        : IRequestHandler<ConfirmEmailCommand, ApiResponse<UserResponseDto>>
    {
        public async Task<ApiResponse<UserResponseDto>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await authService.ConfirmEmailAsync(request.userEmail, request.token);
        }
    }
}
