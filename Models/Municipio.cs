using System.ComponentModel.DataAnnotations.Schema;

namespace TuLote.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [ForeignKey("Provincia")]
        public int Provincia_Id { get; set; }
        public Provincia Provincia { get; set; }
        public List<Municipio> Municipios { get; set; }

        public ICollection<Localidad> Localidades { get; set; }
    }
}
