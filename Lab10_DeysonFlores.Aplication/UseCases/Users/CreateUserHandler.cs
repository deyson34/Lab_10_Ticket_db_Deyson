using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Lab10_DeysonFlores.Models;
using Lab10_DeysonFlores.Application.Interfaces;
using Lab10_DeysonFlores.Application.Services;

namespace Lab10_DeysonFlores.Application.UseCases.Users;

internal class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // 1) Validar (ej: verificar si username ya existe)
        // var existing = await _userRepository.GetByUsernameAsync(request.Username);
        // if (existing != null) throw new InvalidOperationException("Username already exists");

        // 2) Crear entidad y mapear propiedades (personalizar si tiene más campos)
        var user = new User
        {
            UserId = Guid.NewGuid(), // <-- Línea que puedes personalizar según tu estrategia de id
            Username = request.Username,
            PasswordHash = _passwordHasher.Hash(request.Password), // <-- Inyección del IPasswordHasher
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        // 3) Llamar al repositorio para persistir
        await _userRepository.AddAsync(user);

        // 4) Retornar el id creado
        return user.UserId;
    }
}
