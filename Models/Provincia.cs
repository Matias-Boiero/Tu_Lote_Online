using System.ComponentModel.DataAnnotations;

namespace TuLote.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre de Provincia")]
        public string Nombre { get; set; }
        public List<Provincia> Provincias { get; set; }

        public ICollection<Municipio> Municipios { get; set; }



    }
}
