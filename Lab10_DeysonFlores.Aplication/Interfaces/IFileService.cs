using System.Threading.Tasks;

namespace Lab10_DeysonFlores.Application.Services;

public interface IFileService
{
    Task<string> SaveFileAsync(byte[] content, string fileName);
}

