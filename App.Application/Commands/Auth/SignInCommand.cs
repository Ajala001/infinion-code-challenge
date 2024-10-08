using App.Domain.DTOs.Requests;
using App.Domain.DTOs.Responses;
using App.Domain.Interfaces.Services;
using MediatR;

namespace App.Application.Commands.Auth
{
    public record SignInCommand(SignInDto SignInRequest) : IRequest<ApiResponse<string>>;

    public class SignInCommandHandler(IAuthService authService) : IRequestHandler<SignInCommand, ApiResponse<string>>
    {
        public Task<ApiResponse<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return authService.SignInAsync(request.SignInRequest);
        }
    }
}
