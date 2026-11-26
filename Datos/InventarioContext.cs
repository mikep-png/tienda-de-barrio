
using Microsoft.EntityFrameworkCore;
using Modelos;
using System.IO;

namespace Datos;

public class InventarioContext : DbContext
{
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string carpeta = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "TiendaDeBarrio");

            Directory.CreateDirectory(carpeta);

            string ruta = Path.Combine(carpeta, "tienda.db");

            optionsBuilder.UseSqlite($"Data Source={ruta}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entidad =>
        {
            entidad.ToTable("Categorias");

            entidad.HasKey(c => c.Id);

            entidad.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(80);

            entidad.Property(c => c.Descripcion)
                .HasMaxLength(200);
        });

        modelBuilder.Entity<Producto>(entidad =>
        {
            entidad.ToTable("Productos");

            entidad.HasKey(p => p.Id);

            entidad.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(120);

            entidad.Property(p => p.Precio)
                .IsRequired();

            entidad.Property(p => p.Stock)
                .IsRequired();

            entidad.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
