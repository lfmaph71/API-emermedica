using System.ComponentModel.DataAnnotations;

namespace AppEmermedica.Application.Usuarios.Requests
{
    public class GetUsuarioByNameRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
        public string Nombre { get; set; }
    }
}
