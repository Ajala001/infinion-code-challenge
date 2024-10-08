using App.Application.IExternalServices;
using App.Domain.Interfaces.Repositories;
using App.infrastructure.DataContext;
using App.infrastructure.ExternalServices;
using App.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.infrastructure
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<User_ProductDb>(options =>
            {
                options.UseMySQL(configuration.GetConnectionString("User-ProductConnectionString"));
            });

            // Register Repositories
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
        
            return services;
        }
    }
}
