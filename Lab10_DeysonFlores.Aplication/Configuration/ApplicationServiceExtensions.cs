using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MediatR;
using System.Reflection;

namespace Lab10_DeysonFlores.Application.Configuration;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Registrar MediatR para esta capa; descubrirá los Handlers internos que creamos.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Aquí puedes añadir registros de servicios de aplicación (p. ej. validadores, mappers, comportamientos de MediatR, etc.)

        return services;
    }
}
