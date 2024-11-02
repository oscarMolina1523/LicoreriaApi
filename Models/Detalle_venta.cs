using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Detalle_venta
    {
        [Key]
        public int Id_detalle { get; set; }

        public string cantidad { get; set; }

        public int Id_venta { get; set; }

        public int Id_producto { get; set; }

        public string Descripcion { get; set; }

        public string Sub_total { get; set; }

        public int Total { get; set; }

        //propiedades de navegacion 

        public virtual Venta Venta { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
