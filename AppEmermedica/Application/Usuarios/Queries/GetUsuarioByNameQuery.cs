using AppEmermedica.Domain.Entities;
using MediatR;

namespace AppEmermedica.Application.Usuarios.Queries
{
    public record GetUsuarioByNameQuery(string name) : IRequest<Usuario>;
    
}
