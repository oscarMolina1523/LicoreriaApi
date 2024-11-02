using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Detalle_compra
    {
        [Key]
        public int Id_detalleCompra { get; set; }  

        public string Cantidad { get; set; }

        public string Descripcion { get; set; }

        public int Id_compra { get; set; }

        public string Precio { get; set; }

        public string IVA { get; set; }

        public string Sub_total { get; set; }

        public string Total { get; set; }

        //Propiedades navegacion

        public virtual Compra Compra { get; set; }
    }
}
