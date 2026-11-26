
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelos;

namespace Datos;

public class RepositorioCategorias : IRepositorioCategorias
{
    public Categoria Registrar(Categoria categoria)
    {
        ArgumentNullException.ThrowIfNull(categoria);

        if (string.IsNullOrWhiteSpace(categoria.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        using var db = new InventarioContext();

        categoria.Id = 0;

        db.Categorias.Add(categoria);
        db.SaveChanges();

        return categoria;
    }

    public List<Categoria> Listar()
    {
        using var db = new InventarioContext();

        return db.Categorias
            .AsNoTracking()
            .OrderBy(c => c.Nombre)
            .ToList();
    }

    public Categoria? BuscarPorId(int id)
    {
        using var db = new InventarioContext();

        return db.Categorias
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == id);
    }

    public List<Categoria> BuscarPorNombre(string nombre)
    {
        using var db = new InventarioContext();

        var consulta = db.Categorias.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(nombre))
        {
            string busqueda = nombre.Trim();

            consulta = consulta.Where(c =>
                EF.Functions.Like(c.Nombre, $"%{busqueda}%"));
        }

        return consulta.OrderBy(c => c.Nombre).ToList();
    }

    public bool Modificar(Categoria categoria)
    {
        ArgumentNullException.ThrowIfNull(categoria);

        if (string.IsNullOrWhiteSpace(categoria.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        using var db = new InventarioContext();

        var existente = db.Categorias.Find(categoria.Id);

        if (existente == null)
            return false;

        existente.Nombre = categoria.Nombre.Trim();
        existente.Descripcion = categoria.Descripcion?.Trim() ?? "";

        db.SaveChanges();

        return true;
    }

    public bool Eliminar(int id)
    {
        using var db = new InventarioContext();

        var existente = db.Categorias.Find(id);

        if (existente == null)
            return false;

        bool tieneProductos = db.Productos
            .Any(p => p.IdCategoria == id);

        if (tieneProductos)
            throw new InvalidOperationException(
                "No se puede eliminar una categoría que tiene productos.");

        db.Categorias.Remove(existente);
        db.SaveChanges();

        return true;
    }
}
