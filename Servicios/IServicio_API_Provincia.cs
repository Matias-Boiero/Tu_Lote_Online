using TuLote.Models;

namespace TuLote.Servicios
{
    public interface IServicio_API_Provincia
    {
        Task<List<Provincia>> Lista();
        //Task<Provincia> Obtener(int idProducto);

        //Task<bool> Guardar(Provincia objeto);

        //Task<bool> Editar(int idProvincia, Provincia objeto);

        //Task<bool> Eliminar(int idProvincia);
    }
}
