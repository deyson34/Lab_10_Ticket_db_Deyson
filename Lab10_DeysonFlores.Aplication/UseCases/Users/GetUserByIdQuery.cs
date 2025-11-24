using MediatR;
using System;
using Lab10_DeysonFlores.Models.Dtos;

namespace Lab10_DeysonFlores.Application.UseCases.Users;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;

