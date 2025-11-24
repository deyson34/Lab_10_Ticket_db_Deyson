// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Aplication\Services\ITokenService.cs
namespace Lab10_DeysonFlores.Application.Services;

public interface ITokenService
{
    string GenerateToken(Guid userId, string username);
}

