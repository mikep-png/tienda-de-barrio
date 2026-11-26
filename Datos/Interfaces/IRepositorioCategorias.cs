
using Modelos;

namespace Datos.Interfaces;

public interface IRepositorioCategorias
{
    Categoria Registrar(Categoria categoria);
    List<Categoria> Listar();
    Categoria? BuscarPorId(int id);
    List<Categoria> BuscarPorNombre(string nombre);
    bool Modificar(Categoria categoria);
    bool Eliminar(int id);
}
