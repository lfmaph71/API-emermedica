using AppEmermedica.Domain.Entities;
using MediatR;

namespace AppEmermedica.Application.Usuarios.Commands
{
    public record CreateUsuarioCommand(Usuario usuario): IRequest<Usuario>;
    
}
