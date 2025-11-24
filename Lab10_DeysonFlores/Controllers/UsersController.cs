// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores\Controllers\UsersController.cs
using Lab10_DeysonFlores.Models.Dtos;
using Lab10_DeysonFlores.Application.Services;
using Lab10_DeysonFlores.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Lab10_DeysonFlores.Application.Interfaces;
using Lab10_DeysonFlores.Models;

namespace Lab10_DeysonFlores.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService; // agregado

    public UsersController(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService; // asignar
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        var dtos = users.Select(u => new UserDto { UserId = u.UserId, Username = u.Username, Email = u.Email });
        return Ok(dtos);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        var existing = await _userRepository.GetByUsernameAsync(request.Username);
        if (existing != null) return BadRequest(new { message = "Username already exists" });

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = _passwordHasher.Hash(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        // Generar token inmediatamente después del registro
        var token = _tokenService.GenerateToken(user.UserId, user.Username);
        var authResponse = new AuthResponse { Token = token, Username = user.Username };

        return CreatedAtAction(nameof(GetAll), new { id = user.UserId }, authResponse);
    }
}
