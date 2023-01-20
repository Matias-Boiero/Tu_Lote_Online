
using TuLote.Models;

namespace TuLote.Servicios
{
    public interface IServicio_API_Municipio
    {
        Task<List<Municipio>> Lista();
        // Task<Municipio> Obtener(int idMunicipio);

        //Task<bool> Guardar(Municipio objeto);

        //Task<bool> Editar(int idMunicipio, Municipio objeto);

        //Task<bool> Eliminar(int idMunicipio);
    }
}
