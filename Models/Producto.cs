using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Producto
    {
        [Key]
        public int CodigoProducto { get; set; }

        public string Nombre { get; set; }

        public string descripcion { get; set; }

        public DateTime FechaIngreso { get; set; }

        public bool Estado { get; set; }

        public int CodigoCategoria { get; set; }

        public int Id_bodega { get; set; }

        //propiedades navegacion

        public virtual Categoria Categoria { get; set; }


        //    public virtual ICollection< Bodega >Bodegas { get; set; }

        public virtual Bodega Bodegas { get; set; }

        // Propiedad de navegación
        public virtual ICollection<Detalle_venta> DetallesVentas { get; set; } = new List<Detalle_venta>();

    }
}
