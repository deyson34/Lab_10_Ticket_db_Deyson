// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Infrastructure\Services\AuthService.cs
using System.Threading.Tasks;
using Lab10_DeysonFlores.Application.Services;

namespace Lab10_DeysonFlores.Infrastructure.Services;

public class AuthService : IAuthService
{
    public Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        // Stub mínimo: siempre devuelve false. Reemplazar por lógica real.
        return Task.FromResult(false);
    }
}

