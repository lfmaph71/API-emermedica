using AppEmermedica.Application.Interfaces;
using AppEmermedica.Domain.Entities;
using AppEmermedica.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AppEmermedica.Infraestructura.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        private static readonly Regex AllowedNamePattern = new(@"^[\p{L}\p{M}\d\s]+$", RegexOptions.Compiled);

        public async Task<Usuario> Create(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre del usuario es obligatorio.", nameof(usuario.Nombre));

            if (!AllowedNamePattern.IsMatch(usuario.Nombre))
                throw new ArgumentException("El nombre solo puede contener letras, números y espacios.", nameof(usuario.Nombre));

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                throw new ArgumentException("El rol del usuario es obligatorio.", nameof(usuario.Rol));

            if (!Regex.IsMatch(usuario.Rol, "^(Admin|User|Guest)$"))
                throw new ArgumentException("Rol inválido. Use Admin, User o Guest.", nameof(usuario.Rol));

            using var transaction = await _context.Database.BeginTransactionAsync();

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
            return usuario;
        }

        public async Task<Usuario?> GetUsuariosByName(string nombre)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre);
        }
    }
}
