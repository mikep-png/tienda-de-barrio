
using System.ComponentModel.DataAnnotations;

namespace Modelos;

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [StringLength(120)]
    public string Nombre { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    // Clave foránea.
    public int IdCategoria { get; set; }

    // Relación: cada producto pertenece a una categoría.
    public Categoria? Categoria { get; set; }
}
