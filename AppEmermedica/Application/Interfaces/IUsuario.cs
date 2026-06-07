using AppEmermedica.Domain.Entities;

namespace AppEmermedica.Application.Interfaces
{
    public interface IUsuario
    {
        Task<Usuario?> GetUsuariosByName(string nombre);
        Task<Usuario> Create(Usuario usuario);
    }
}
