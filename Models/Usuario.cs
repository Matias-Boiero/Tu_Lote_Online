using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TuLote.Models
{
    public class Usuario : IdentityUser
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(3)]
        public string Alias { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El mail debe ser válido")]
        public string Email { get; set; }

    }
}
