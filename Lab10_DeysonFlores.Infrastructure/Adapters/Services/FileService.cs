// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Infrastructure\Services\FileService.cs

using Lab10_DeysonFlores.Application.Services;

namespace Lab10_DeysonFlores.Infrastructure.Services;

public class FileService : IFileService
{
    public Task<string> SaveFileAsync(byte[] content, string fileName)
    {
        // Implementación mínima: en producción guarda en disco o blob y devuelve la ruta/URL
        var fakePath = $"/files/{fileName}";
        return Task.FromResult(fakePath);
    }
}
