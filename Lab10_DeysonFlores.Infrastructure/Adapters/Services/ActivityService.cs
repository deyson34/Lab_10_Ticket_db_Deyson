// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Infrastructure\Services\ActivityService.cs

using Lab10_DeysonFlores.Application.Services;

namespace Lab10_DeysonFlores.Infrastructure.Services;

public class ActivityService : IActivityService
{
    public Task LogAsync(string message)
    {
        // Stub mínimo: en producción guardaría en BD o servicio de logging
        Console.WriteLine($"Activity: {message}");
        return Task.CompletedTask;
    }
}
