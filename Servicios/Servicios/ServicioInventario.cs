
using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Datos.Interfaces;
using Modelos;

namespace Servicios;

public class ServicioInventario
{
    private readonly IRepositorioProductos _repositorioProductos;
    private readonly IRepositorioCategorias _repositorioCategorias;

    public ServicioInventario()
        : this(new RepositorioProductos(), new RepositorioCategorias())
    {
    }

    public ServicioInventario(
        IRepositorioProductos repositorioProductos,
        IRepositorioCategorias repositorioCategorias)
    {
        _repositorioProductos = repositorioProductos
            ?? throw new ArgumentNullException(nameof(repositorioProductos));

        _repositorioCategorias = repositorioCategorias
            ?? throw new ArgumentNullException(nameof(repositorioCategorias));
    }

    // REGISTRAR PRODUCTO
    public Producto RegistrarProducto(Producto producto)
    {
        ValidarProducto(producto);

        if (_repositorioCategorias.BuscarPorId(producto.IdCategoria) == null)
            throw new InvalidOperationException(
                "La categoría seleccionada no existe.");

        producto.Nombre = producto.Nombre.Trim();

        return _repositorioProductos.Registrar(producto);
    }

    // BUSCAR PRODUCTO POR ID
    public Producto BuscarProductoPorId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID debe ser válido.");

        return _repositorioProductos.BuscarPorId(id)
            ?? throw new KeyNotFoundException("El producto no existe.");
    }

    // BUSCAR PRODUCTOS POR NOMBRE
    public List<Producto> BuscarProductosPorNombre(string nombre)
    {
        return _repositorioProductos.BuscarPorNombre(nombre);
    }

    // MODIFICAR PRODUCTO
    public bool ModificarProducto(Producto producto)
    {
        ValidarProducto(producto);

        if (_repositorioProductos.BuscarPorId(producto.Id) == null)
            throw new KeyNotFoundException("El producto no existe.");

        if (_repositorioCategorias.BuscarPorId(producto.IdCategoria) == null)
            throw new InvalidOperationException(
                "La categoría seleccionada no existe.");

        producto.Nombre = producto.Nombre.Trim();

        return _repositorioProductos.Modificar(producto);
    }

    // ELIMINAR PRODUCTO
    public bool EliminarProducto(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El ID debe ser válido.");

        if (_repositorioProductos.BuscarPorId(id) == null)
            throw new KeyNotFoundException("El producto no existe.");

        return _repositorioProductos.Eliminar(id);
    }

    // CONSULTAR TODO EL INVENTARIO
    public List<Producto> ConsultarInventario()
    {
        return _repositorioProductos.Listar();
    }

    // CONSULTAR PRODUCTOS POR CATEGORÍA
    public List<Producto> ConsultarProductosPorCategoria(int idCategoria)
    {
        if (_repositorioCategorias.BuscarPorId(idCategoria) == null)
            throw new KeyNotFoundException("La categoría no existe.");

        return _repositorioProductos.BuscarPorCategoria(idCategoria);
    }

    // AGREGAR EXISTENCIAS
    public bool AgregarExistencias(int idProducto, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException(
                "La cantidad debe ser mayor que cero.");

        Producto producto = BuscarProductoPorId(idProducto);

        if (producto.Stock > int.MaxValue - cantidad)
            throw new InvalidOperationException(
                "La cantidad supera el límite permitido.");

        producto.Stock += cantidad;

        return _repositorioProductos.Modificar(producto);
    }

    // REGISTRAR VENTA Y DESCONTAR EXISTENCIAS
    public bool RegistrarVenta(int idProducto, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException(
                "La cantidad de venta debe ser mayor que cero.");

        Producto producto = BuscarProductoPorId(idProducto);

        if (cantidad > producto.Stock)
            throw new InvalidOperationException(
                "No hay suficientes existencias para realizar la venta.");

        producto.Stock -= cantidad;

        return _repositorioProductos.Modificar(producto);
    }

    // REPORTE 1: PRODUCTOS CON STOCK BAJO
    public List<Producto> ConsultarProductosStockBajo(int stockMinimo)
    {
        if (stockMinimo < 0)
            throw new ArgumentException(
                "El stock mínimo no puede ser negativo.");

        return _repositorioProductos.Listar()
            .Where(p => p.Stock <= stockMinimo)
            .OrderBy(p => p.Stock)
            .ToList();
    }

    // REPORTE 2: INVENTARIO AGRUPADO POR CATEGORÍA
    public List<ResumenCategoria> ConsultarInventarioPorCategoria()
    {
        return _repositorioProductos.Listar()
            .GroupBy(p => p.Categoria?.Nombre ?? "Sin categoría")
            .Select(grupo => new ResumenCategoria
            {
                Categoria = grupo.Key,
                CantidadProductos = grupo.Count(),
                UnidadesDisponibles = grupo.Sum(p => p.Stock),
                ValorInventario = grupo.Sum(p => p.Precio * p.Stock)
            })
            .OrderBy(r => r.Categoria)
            .ToList();
    }

    // VALIDAR PRODUCTOS
    private static void ValidarProducto(Producto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);

        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new ArgumentException(
                "El nombre del producto es obligatorio.");

        if (producto.Precio <= 0)
            throw new ArgumentException(
                "El precio debe ser mayor que cero.");

        if (producto.Stock < 0)
            throw new ArgumentException(
                "El stock no puede ser negativo.");

        if (producto.IdCategoria <= 0)
            throw new ArgumentException(
                "Debe seleccionar una categoría.");
    }
}

// ESTRUCTURA PARA EL SEGUNDO REPORTE
public class ResumenCategoria
{
    public string Categoria { get; set; } = string.Empty;
    public int CantidadProductos { get; set; }
    public int UnidadesDisponibles { get; set; }
    public decimal ValorInventario { get; set; }
}
