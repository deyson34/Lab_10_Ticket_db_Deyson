// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores\Repositories\IUserRepository.cs
using Lab10_DeysonFlores.Models;

namespace Lab10_DeysonFlores.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}
