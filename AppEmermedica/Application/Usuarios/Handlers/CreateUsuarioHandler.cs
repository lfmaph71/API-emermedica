using AppEmermedica.Application.Interfaces;
using AppEmermedica.Application.Usuarios.Commands;
using AppEmermedica.Domain.Entities;
using MediatR;

namespace AppEmermedica.Application.Usuarios.Handlers
{
    public class CreateUsuarioHandler : IRequestHandler<CreateUsuarioCommand, Usuario>
    {
        private readonly IUsuario _usuarioService;

        public CreateUsuarioHandler(IUsuario usuario)
        {
           _usuarioService = usuario;
        }

        public async Task<Usuario> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioService.Create(request.usuario);
        }

    }
}
