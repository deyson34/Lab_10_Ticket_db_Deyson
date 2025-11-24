// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Infrastructure\Services\UploadFileToAzureStorageService.cs

using Lab10_DeysonFlores.Application.Services;

namespace Lab10_DeysonFlores.Infrastructure.Services;

public class UploadFileToAzureStorageService : IUploadFileToAzureStorageService
{
    public Task<string> UploadAsync(byte[] content, string fileName)
    {
        // Stub mínimo: simula subir y devuelve una URL falsa
        var fakeUrl = $"https://storage.example.com/{fileName}";
        return Task.FromResult(fakeUrl);
    }
}
