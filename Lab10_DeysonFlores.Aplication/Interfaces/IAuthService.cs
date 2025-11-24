using System.Threading.Tasks;

namespace Lab10_DeysonFlores.Application.Services;

public interface IAuthService
{
    Task<bool> ValidateCredentialsAsync(string username, string password);
}

