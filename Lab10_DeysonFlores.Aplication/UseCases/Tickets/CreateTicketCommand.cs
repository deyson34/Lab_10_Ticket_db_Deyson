// ...existing code...
using MediatR;
using System;

namespace Lab10_DeysonFlores.Application.UseCases.Tickets;

public record CreateTicketCommand(Guid UserId, string Title, string? Description) : IRequest<Guid>;
// ...existing code...
