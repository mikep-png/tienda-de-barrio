
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelos;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(80)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(200)]
    public string Descripcion { get; set; } = string.Empty;

    // Relación: una categoría puede tener muchos productos.
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
