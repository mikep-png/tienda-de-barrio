
using Microsoft.EntityFrameworkCore;
using Modelos;

namespace Datos;

public static class InicializadorBaseDatos
{
    public static void Inicializar()
    {
        using var db = new InventarioContext();

        db.Database.EnsureCreated();

        if (db.Categorias.Any())
            return;

        var categorias = new List<Categoria>
        {
            new()
            {
                Nombre = "Abarrotes",
                Descripcion = "Productos básicos de la tienda"
            },
            new()
            {
                Nombre = "Bebidas",
                Descripcion = "Bebidas y refrescos"
            },
            new()
            {
                Nombre = "Aseo",
                Descripcion = "Productos de limpieza y aseo"
            },
            new()
            {
                Nombre = "Snacks",
                Descripcion = "Mecatos y pasabocas"
            }
        };

        db.Categorias.AddRange(categorias);
        db.SaveChanges();

        var productos = new List<Producto>
        {
            new()
            {
                Nombre = "Arroz 1 kg",
                Precio = 4500,
                Stock = 20,
                IdCategoria = categorias[0].Id
            },
            new()
            {
                Nombre = "Azúcar 1 kg",
                Precio = 4200,
                Stock = 15,
                IdCategoria = categorias[0].Id
            },
            new()
            {
                Nombre = "Gaseosa 1.5 L",
                Precio = 6500,
                Stock = 12,
                IdCategoria = categorias[1].Id
            },
            new()
            {
                Nombre = "Jabón en polvo",
                Precio = 7800,
                Stock = 5,
                IdCategoria = categorias[2].Id
            },
            new()
            {
                Nombre = "Papas fritas",
                Precio = 2500,
                Stock = 3,
                IdCategoria = categorias[3].Id
            }
        };

        db.Productos.AddRange(productos);
        db.SaveChanges();
    }
}
