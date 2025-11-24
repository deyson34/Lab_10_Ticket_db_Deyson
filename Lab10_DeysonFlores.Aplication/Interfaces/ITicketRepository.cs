// ...existing code...
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab10_DeysonFlores.Models;

namespace Lab10_DeysonFlores.Application.Interfaces;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<Ticket?> GetByIdAsync(Guid id);
    Task AddAsync(Ticket ticket);
}
// ...existing code...
