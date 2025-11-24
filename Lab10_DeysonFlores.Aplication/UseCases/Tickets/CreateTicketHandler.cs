using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Lab10_DeysonFlores.Models;
using Lab10_DeysonFlores.Application.Interfaces;

namespace Lab10_DeysonFlores.Application.UseCases.Tickets;

internal class CreateTicketHandler : IRequestHandler<CreateTicketCommand, Guid>
{
    private readonly ITicketRepository _ticketRepository;

    public CreateTicketHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        // 1) Validar (ej: verificar si user existe) -> inyectar IUserRepository si lo requieres
        // 2) Crear entidad y mapear propiedades (personalizar si tiene más campos)
        var ticket = new Ticket
        {
            TicketId = Guid.NewGuid(), // <-- Personalizar si usas otra estrategia de id
            UserId = request.UserId, // <-- Línea a personalizar si el FK tiene otro nombre
            Title = request.Title,
            Description = request.Description,
            Status = "Open", // <-- Personalizar estados
            CreatedAt = DateTime.UtcNow
        };

        // 3) Persistir
        await _ticketRepository.AddAsync(ticket);

        // 4) Retornar id
        return ticket.TicketId;
    }
}
