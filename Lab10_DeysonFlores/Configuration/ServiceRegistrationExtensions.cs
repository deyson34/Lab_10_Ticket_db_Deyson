using System.Text;
using Lab10_DeysonFlores.Infrastructure.Configuration;
using Lab10_DeysonFlores.Models;
using Lab10_DeysonFlores.Repositories;
using Lab10_DeysonFlores.Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lab10_DeysonFlores.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // Registrar el servicio IClientContextProvider para acceder a los headers de cada request
        services.AddScoped<IClientContextProvider, ClientContextProvider>();

        // Registro de servicios de infraestructura (asume que existe la extensión)
        services.AddInfrastructureServices(configuration);

        // Registrar servicios de la capa Application (hashing, token, casos de uso)
        services.AddApplicationLayer(configuration);

        // Configuración de autenticación JWT
        var secretKey = configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("Jwt:SecretKey no está configurado.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"] ?? "local",
                        ValidAudience = configuration["Jwt:Audience"] ?? "local",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

        // Habilitar controladores
        services.AddControllers();

        // Nota: la configuración de Swagger (AddSwaggerGen/SwaggerDoc) se centraliza en AddCustomOpenApi()

        return services;
    }
}

// Interfaz y clase de ejemplo (si no existen en el proyecto)
public interface IClientContextProvider
{
    string GetHeader(string name);
}

public class ClientContextProvider : IClientContextProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClientContextProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetHeader(string name)
    {
        return _httpContextAccessor.HttpContext?.Request?.Headers[name].ToString() ?? string.Empty;
    }
}
