// ...existing code...
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Lab10_DeysonFlores.Application.Interfaces;
using Lab10_DeysonFlores.Models.Dtos;

namespace Lab10_DeysonFlores.Application.UseCases.Users;

internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null) return null;

        // Mapear manualmente a UserDto (personalizar según propiedades)
        var dto = new UserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email
        };

        return dto;
    }
}
