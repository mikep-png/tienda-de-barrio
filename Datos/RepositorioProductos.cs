
using Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelos;

namespace Datos;

public class RepositorioProductos : IRepositorioProductos
{
    public Producto Registrar(Producto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);

        Validar(producto);

        using var db = new InventarioContext();

        bool categoriaExiste = db.Categorias
            .Any(c => c.Id == producto.IdCategoria);

        if (!categoriaExiste)
            throw new ArgumentException("La categoría seleccionada no existe.");

        producto.Id = 0;
        producto.Categoria = null;

        db.Productos.Add(producto);
        db.SaveChanges();

        return producto;
    }

    public List<Producto> Listar()
    {
        using var db = new InventarioContext();

        return db.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .OrderBy(p => p.Nombre)
            .ToList();
    }

    public Producto? BuscarPorId(int id)
    {
        using var db = new InventarioContext();

        return db.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .FirstOrDefault(p => p.Id == id);
    }

    public List<Producto> BuscarPorNombre(string nombre)
    {
        using var db = new InventarioContext();

        var consulta = db.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nombre))
        {
            string busqueda = nombre.Trim();

            consulta = consulta.Where(p =>
                EF.Functions.Like(p.Nombre, $"%{busqueda}%"));
        }

        return consulta.OrderBy(p => p.Nombre).ToList();
    }

    public List<Producto> BuscarPorCategoria(int idCategoria)
    {
        using var db = new InventarioContext();

        return db.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Where(p => p.IdCategoria == idCategoria)
            .ToList();
    }

    public bool Modificar(Producto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);

        Validar(producto);

        using var db = new InventarioContext();

        var existente = db.Productos.Find(producto.Id);

        if (existente == null)
            return false;

        bool categoriaExiste = db.Categorias
            .Any(c => c.Id == producto.IdCategoria);

        if (!categoriaExiste)
            throw new ArgumentException("La categoría seleccionada no existe.");

        existente.Nombre = producto.Nombre.Trim();
        existente.Precio = producto.Precio;
        existente.Stock = producto.Stock;
        existente.IdCategoria = producto.IdCategoria;

        db.SaveChanges();

        return true;
    }

    public bool Eliminar(int id)
    {
        using var db = new InventarioContext();

        var existente = db.Productos.Find(id);

        if (existente == null)
            return false;

        db.Productos.Remove(existente);
        db.SaveChanges();

        return true;
    }

    private static void Validar(Producto producto)
    {
        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new ArgumentException("El nombre del producto es obligatorio.");

        if (producto.Precio <= 0)
            throw new ArgumentException("El precio debe ser mayor que cero.");

        if (producto.Stock < 0)
            throw new ArgumentException("El stock no puede ser negativo.");

        if (producto.IdCategoria <= 0)
            throw new ArgumentException("Debe seleccionar una categoría.");
    }
}
