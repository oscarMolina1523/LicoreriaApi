using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Categoria
    {
        [Key]
        public int CodigoCategoria { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaIngreso { get; set; }

        public bool Estado { get; set; }

        // Propiedad de navegación para los productos
        public virtual ICollection<Producto> Productos { get; set; } 

    }
}
