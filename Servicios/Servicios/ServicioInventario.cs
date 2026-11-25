<<<<<<< HEAD
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Modelos;

namespace Servicios
{
    public class ServicioInventario
    {
        private readonly RepositorioProductos _repositorioProductos;
        private readonly RepositorioCategorias _repositorioCategorias;

        public ServicioInventario()
        {
            _repositorioProductos = new RepositorioProductos();
            _repositorioCategorias = new RepositorioCategorias();
        }

        public ServicioInventario(
            RepositorioProductos repositorioProductos,
            RepositorioCategorias repositorioCategorias)
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
            {
                throw new InvalidOperationException(
                    "La categoría seleccionada no existe.");
            }

            producto.Nombre = producto.Nombre.Trim();

            return _repositorioProductos.Registrar(producto);
        }

        // BUSCAR PRODUCTO POR ID
        public Producto BuscarProductoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser válido.");

            return _repositorioProductos.BuscarPorId(id)
                ?? throw new KeyNotFoundException(
                    "El producto no existe.");
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

        // REGISTRAR VENTA
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

        // CONSULTAR PRODUCTOS CON STOCK BAJO
        public List<Producto> ConsultarProductosStockBajo(int stockMinimo)
        {
            if (stockMinimo < 0)
                throw new ArgumentException(
                    "El stock mínimo no puede ser negativo.");

            return _repositorioProductos.Listar()
                .Where(p => p.Stock <= stockMinimo)
                .ToList();
        }

        // VALIDAR DATOS DEL PRODUCTO
        private void ValidarProducto(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException(
                    "El nombre del producto es obligatorio.");

            if (producto.Precio < 0)
                throw new ArgumentException(
                    "El precio no puede ser negativo.");

            if (producto.Stock < 0)
                throw new ArgumentException(
                    "El stock no puede ser negativo.");

            if (producto.IdCategoria <= 0)
                throw new ArgumentException(
                    "Debe seleccionar una categoría.");
        }
=======
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    internal class ServicioInventario
    {
>>>>>>> 2694a36c74f293d92b2c16a095450b8129786946
    }
}
