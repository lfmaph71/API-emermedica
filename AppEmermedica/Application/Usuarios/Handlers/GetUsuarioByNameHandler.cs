using AppEmermedica.Application.Interfaces;
using AppEmermedica.Application.Usuarios.Commands;
using AppEmermedica.Application.Usuarios.Queries;
using AppEmermedica.Domain.Entities;
using MediatR;

namespace AppEmermedica.Application.Usuarios.Handlers
{
    public class GetUsuarioByNameHandler : IRequestHandler<GetUsuarioByNameQuery,Usuario>
    {
        private readonly IUsuario _usuarioService;

        public GetUsuarioByNameHandler(IUsuario usuario)
        {
            _usuarioService = usuario;
        }

        public async Task<Usuario?> Handle(GetUsuarioByNameQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioService.GetUsuariosByName(request.name);
        }

    }
}
