using TuLote.Models;

namespace TuLote.Servicios
{
    public interface IServicio_API_Localidad
    {
        Task<List<Localidad>> Lista();
        //Task<Localidad> Obtener(int idLocalidad);

        //Task<bool> Guardar(Localidad objeto);

        //Task<bool> Editar(int idLocalidad, Localidad objeto);

        //Task<bool> Eliminar(int idLocalidad);
    }
}
