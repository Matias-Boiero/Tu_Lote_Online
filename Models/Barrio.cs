using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuLote.Models
{
    public class Barrio
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del barrio es requerido")]
        [Display(Name = "Nombre del barrio")]
        public string Nombre { get; set; }
        [ForeignKey("Localidad")]
        public long Localidad_Id { get; set; }
        public Localidad Localidad { get; set; }
    }
}
