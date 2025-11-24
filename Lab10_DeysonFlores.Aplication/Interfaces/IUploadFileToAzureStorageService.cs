using System.Threading.Tasks;

namespace Lab10_DeysonFlores.Application.Services;

public interface IUploadFileToAzureStorageService
{
    Task<string> UploadAsync(byte[] content, string fileName);
}

