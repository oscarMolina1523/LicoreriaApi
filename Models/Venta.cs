using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Venta
    {
        [Key]
        public int Id_venta { get; set; }

        public string IVA { get; set; }

        public string Nombre_cliente { get; set; }

        public int Idusuario { get; set; }

        public string Sub_total { get; set; }

         public string Total { get; set; }

        public int IdCliente { get; set; }

        public bool Estado {  get; set; }

        //propiedades 

        public virtual Usuario Usuario { get; set; }

        public virtual Cliente Cliente { get; set; }

        // Propiedad de navegación
        public virtual ICollection<Detalle_venta> DetallesVentas { get; set; } = new List<Detalle_venta>();
    }
}
