using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Commands.Auth
{
    public record SignUpCommand(SignUpDto SignUpRequest) : IRequest<ApiResponse<UserResponseDto>>;

    public class SignUpCommandHandler(IAuthService authService) :
        IRequestHandler<SignUpCommand, ApiResponse<UserResponseDto>>
    {
        public async Task<ApiResponse<UserResponseDto>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            return await authService.SignUpAsync(request.SignUpRequest);
        }
    }
}
