using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Datos
{
    public class RepositorioCategorias
    {
        private readonly string _rutaArchivo;
        private readonly List<Categoria> _categorias;
        private readonly JsonSerializerOptions _jsonOptions;

        public RepositorioCategorias(string? rutaArchivo = null)
        {
            _rutaArchivo = rutaArchivo ?? Path.Combine(AppContext.BaseDirectory, "DatosJson", "categorias.json");
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            _categorias = CargarDesdeArchivo();
        }

        public Categoria Registrar(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            if (categoria.Id <= 0 || _categorias.Any(c => c.Id == categoria.Id))
            {
                categoria.Id = _categorias.Count == 0 ? 1 : _categorias.Max(c => c.Id) + 1;
            }

            _categorias.Add(categoria);
            GuardarEnArchivo();
            return categoria;
        }

        public List<Categoria> Listar()
        {
            return _categorias.ToList();
        }

        public Categoria? BuscarPorId(int id)
        {
            return _categorias.FirstOrDefault(c => c.Id == id);
        }

        public List<Categoria> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return _categorias.ToList();
            }

            return _categorias
                .Where(c => c.Nombre.Contains(nombre.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool Modificar(Categoria categoriaActualizada)
        {
            if (categoriaActualizada == null)
            {
                throw new ArgumentNullException(nameof(categoriaActualizada));
            }

            Categoria? existente = _categorias.FirstOrDefault(c => c.Id == categoriaActualizada.Id);
            if (existente == null)
            {
                return false;
            }

            existente.Nombre = categoriaActualizada.Nombre;
            existente.Descripcion = categoriaActualizada.Descripcion;
            GuardarEnArchivo();
            return true;
        }

        public bool Eliminar(int id)
        {
            Categoria? existente = _categorias.FirstOrDefault(c => c.Id == id);
            if (existente == null)
            {
                return false;
            }

            _categorias.Remove(existente);
            GuardarEnArchivo();
            return true;
        }

        private List<Categoria> CargarDesdeArchivo()
        {
            try
            {
                if (!File.Exists(_rutaArchivo))
                {
                    return new List<Categoria>();
                }

                string json = File.ReadAllText(_rutaArchivo, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Categoria>();
                }

                return JsonSerializer.Deserialize<List<Categoria>>(json, _jsonOptions) ?? new List<Categoria>();
            }
            catch
            {
                return new List<Categoria>();
            }
        }

        private void GuardarEnArchivo()
        {
            string? directorio = Path.GetDirectoryName(_rutaArchivo);
            if (!string.IsNullOrWhiteSpace(directorio) && !Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            string json = JsonSerializer.Serialize(_categorias, _jsonOptions);
            File.WriteAllText(_rutaArchivo, json, Encoding.UTF8);
        }
    }
}