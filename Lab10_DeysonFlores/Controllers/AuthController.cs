// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores\Controllers\AuthController.cs

using Lab10_DeysonFlores.Application.Interfaces;
using Lab10_DeysonFlores.Models.Dtos;
using Lab10_DeysonFlores.Application.Services;
using Lab10_DeysonFlores.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_DeysonFlores.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthController(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null) return Unauthorized(new { message = "Usuario o contraseña inválidos" });

        if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
            return Unauthorized(new { message = "Usuario o contraseña inválidos" });

        var token = _tokenService.GenerateToken(user.UserId, user.Username);
        return Ok(new AuthResponse { Token = token, Username = user.Username });
    }
}
