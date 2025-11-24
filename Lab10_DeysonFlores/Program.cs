using Lab10_DeysonFlores.Configuration;
using Lab10_DeysonFlores.Models;
using Lab10_DeysonFlores.Repositories;
using Lab10_DeysonFlores.Application.Services;
using System;
using System.Reflection;
using Lab10_DeysonFlores.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCustomOpenApi();
builder.Services.ConfigureHttpJsonOptions( options =>{
 //DDD
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c  =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab10_DeysonFlores API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed data: crear un usuario de prueba si no existe
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var repo = services.GetRequiredService<IUserRepository>();
        var hasher = services.GetRequiredService<IPasswordHasher>();

        var existing = await repo.GetByUsernameAsync("admin");
        if (existing == null)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = "admin",
                PasswordHash = hasher.Hash("Password123!"),
                Email = "admin@example.com",
                CreatedAt = DateTime.UtcNow
            };
            await repo.AddAsync(user);
            Console.WriteLine("Usuario de prueba 'admin' creado (Password123!)");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al sembrar datos: {ex.Message}");
    }
}

app.Run();
