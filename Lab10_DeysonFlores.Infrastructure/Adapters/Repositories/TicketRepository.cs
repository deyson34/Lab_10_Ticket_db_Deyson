using Lab10_DeysonFlores.Application.Interfaces;
using Lab10_DeysonFlores.Data;
using Lab10_DeysonFlores.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10_DeysonFlores.Infrastructure.Adapters.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly TicketeraBdContext _db;

    public TicketRepository(TicketeraBdContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _db.Tickets.AsNoTracking().ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsync(Guid id)
    {
        return await _db.Tickets.FindAsync(id);
    }

    public async Task AddAsync(Ticket ticket)
    {
        await _db.Tickets.AddAsync(ticket);
        await _db.SaveChangesAsync();
    }
}
