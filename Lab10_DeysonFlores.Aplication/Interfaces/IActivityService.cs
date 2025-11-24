using System.Threading.Tasks;

namespace Lab10_DeysonFlores.Application.Services;

public interface IActivityService
{
    Task LogAsync(string message);
}

