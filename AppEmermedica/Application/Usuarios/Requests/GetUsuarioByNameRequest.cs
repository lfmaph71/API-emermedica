using System.ComponentModel.DataAnnotations;

namespace AppEmermedica.Application.Usuarios.Requests
{
    public class GetUsuarioByNameRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        //[RegularExpression(@"^[\p{L}\p{M}\d\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
        public string Nombre { get; set; }
    }
}
