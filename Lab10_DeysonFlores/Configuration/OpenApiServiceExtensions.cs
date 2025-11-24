using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Lab10_DeysonFlores.Configuration;

public static class OpenApiServiceExtensions
{
    public static IServiceCollection AddCustomOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Lab10_DeysonFlores API",
                Version = "v1"
            });

            // Configuración para soportar JWT en Swagger UI
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Ingrese el token JWT como: Bearer {token}",
                Reference = new OpenApiReference
                {
                    Id = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, System.Array.Empty<string>() }
            });
        });
        
        return services;
    }
}
