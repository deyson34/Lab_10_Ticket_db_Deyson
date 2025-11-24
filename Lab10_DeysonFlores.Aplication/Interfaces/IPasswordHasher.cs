// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Aplication\Services\IPasswordHasher.cs
namespace Lab10_DeysonFlores.Application.Services;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string hash, string password);
}

