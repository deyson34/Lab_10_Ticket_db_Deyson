using System;
using MediatR;

namespace Lab10_DeysonFlores.Application.UseCases.Users;

public record CreateUserCommand(string Username, string Password, string? Email) : IRequest<Guid>;
