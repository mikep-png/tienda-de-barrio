
using Modelos;

namespace Datos.Interfaces;

public interface IRepositorioProductos
{
    Producto Registrar(Producto producto);
    List<Producto> Listar();
    Producto? BuscarPorId(int id);
    List<Producto> BuscarPorNombre(string nombre);
    List<Producto> BuscarPorCategoria(int idCategoria);
    bool Modificar(Producto producto);
    bool Eliminar(int id);
}
