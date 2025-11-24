using Lab10_DeysonFlores.Application.Interfaces;
using Lab10_DeysonFlores.Data;
using Lab10_DeysonFlores.Infrastructure.Services;
using Lab10_DeysonFlores.Repositories;
using Lab10_DeysonFlores.Application.Services;
using Lab10_DeysonFlores.Infrastructure.Adapters.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10_DeysonFlores.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Registrar DbContext y librerías de acceso a datos se mantienen en la capa Infrastructure
        var conn = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TicketeraBdContext>(options =>
        {
            options.UseMySql(conn, ServerVersion.AutoDetect(conn));
        });

        // Registramos solo los servicios de infraestructura (repositorios, unidades de trabajo, servicios de infra)

        // ServicesRegister
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUploadFileToAzureStorageService, UploadFileToAzureStorageService>();
        services.AddScoped<IActivityService, ActivityService>();

        // Registrar implementaciones de la capa Application que viven en Infrastructure/Adapters
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        // Repositorios implementados en Infrastructure (exponen interfaces del Domain)
        services.AddScoped<IUserRepository, UserRepository>();
        // Registrar TicketRepository para satisfacer ITicketRepository en Application
        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }  
}