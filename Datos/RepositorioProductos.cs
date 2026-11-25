using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Modelos;

namespace Datos
{
    public class RepositorioProductos
    {
        private readonly string _rutaArchivo;
        private readonly List<Producto> _productos;
        private readonly JsonSerializerOptions _jsonOptions;

        public RepositorioProductos(string? rutaArchivo = null)
        {
            _rutaArchivo = rutaArchivo ?? Path.Combine(AppContext.BaseDirectory, "DatosJson", "productos.json");
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            _productos = CargarDesdeArchivo();
        }

        public Producto Registrar(Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            if (producto.Id <= 0 || _productos.Any(p => p.Id == producto.Id))
            {
                producto.Id = _productos.Count == 0 ? 1 : _productos.Max(p => p.Id) + 1;
            }

            _productos.Add(producto);
            GuardarEnArchivo();
            return producto;
        }

        public List<Producto> Listar()
        {
            return _productos.ToList();
        }

        public Producto? BuscarPorId(int id)
        {
            return _productos.FirstOrDefault(p => p.Id == id);
        }

        public List<Producto> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return _productos.ToList();
            }

            return _productos
                .Where(p => p.Nombre.Contains(nombre.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Producto> BuscarPorCategoria(int idCategoria)
        {
            return _productos
                .Where(p => p.IdCategoria == idCategoria)
                .ToList();
        }

        public bool Modificar(Producto productoActualizado)
        {
            if (productoActualizado == null)
            {
                throw new ArgumentNullException(nameof(productoActualizado));
            }

            Producto? existente = _productos.FirstOrDefault(p => p.Id == productoActualizado.Id);
            if (existente == null)
            {
                return false;
            }

            existente.Nombre = productoActualizado.Nombre;
            existente.Precio = productoActualizado.Precio;
            existente.Stock = productoActualizado.Stock;
            existente.IdCategoria = productoActualizado.IdCategoria;
            GuardarEnArchivo();
            return true;
        }

        public bool Eliminar(int id)
        {
            Producto? existente = _productos.FirstOrDefault(p => p.Id == id);
            if (existente == null)
            {
                return false;
            }

            _productos.Remove(existente);
            GuardarEnArchivo();
            return true;
        }

        private List<Producto> CargarDesdeArchivo()
        {
            try
            {
                if (!File.Exists(_rutaArchivo))
                {
                    return new List<Producto>();
                }

                string json = File.ReadAllText(_rutaArchivo, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Producto>();
                }

                return JsonSerializer.Deserialize<List<Producto>>(json, _jsonOptions) ?? new List<Producto>();
            }
            catch
            {
                return new List<Producto>();
            }
        }

        private void GuardarEnArchivo()
        {
            string? directorio = Path.GetDirectoryName(_rutaArchivo);
            if (!string.IsNullOrWhiteSpace(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            string json = JsonSerializer.Serialize(_productos, _jsonOptions);
            File.WriteAllText(_rutaArchivo, json, Encoding.UTF8);
        }
    }
}