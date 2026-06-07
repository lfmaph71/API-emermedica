using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppEmermedica.Domain.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[\p{L}\p{M}\d\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("^(Admin|User|Guest)$", ErrorMessage = "Rol inválido. Use Admin, User o Guest.")]
        public string Rol { get; set; }
    }
}
