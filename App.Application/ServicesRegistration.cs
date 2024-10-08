using App.Application.Services;
using App.Application.Validations;
using App.Domain.DTOs.Requests;
using App.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServicesRegistration).Assembly));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<IValidator<SignInDto>, SignInValidator>();
            services.AddTransient<IValidator<SignUpDto>, SignUpValidator>();

            return services;
        }
    }
}
