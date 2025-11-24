// filepath: e:\PROYECTOS RIDER\Lab10_DeysonFlores\Lab10_DeysonFlores.Infrastructure\Services\UnitOfWork.cs

using Lab10_DeysonFlores.Application.Services;

namespace Lab10_DeysonFlores.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    public void Commit()
    {
        // Implementación mínima; en producción usar DbContext.SaveChangesAsync etc.
    }
}
