using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Bodega
    {
        [Key]
        public int Id_bodega { get; set; } 

       public int Id_producto { get; set; }

        public bool Estado { get; set; }

        // Propiedad de navegación para varios productos
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    }
}
