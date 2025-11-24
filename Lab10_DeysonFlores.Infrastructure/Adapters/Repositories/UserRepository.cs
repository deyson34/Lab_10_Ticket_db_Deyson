// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores\Repositories\UserRepository.cs

using Lab10_DeysonFlores.Application.Interfaces;
using Lab10_DeysonFlores.Data;
using Lab10_DeysonFlores.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10_DeysonFlores.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TicketeraBdContext _db;

    public UserRepository(TicketeraBdContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _db.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }
}