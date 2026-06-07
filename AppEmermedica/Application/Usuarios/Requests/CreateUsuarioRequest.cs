using System.ComponentModel.DataAnnotations;

namespace AppEmermedica.Application.Usuarios.Requests
{
    public class CreateUsuarioRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "El rol no puede tener más de 10 caracteres.")]
        [RegularExpression("^(Admin|User|Guest)$", ErrorMessage = "Rol inválido. Use Admin, User o Guest.")]
        public string Rol { get; set; }
    }
}
