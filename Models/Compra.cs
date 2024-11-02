using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Compra
    {
        [Key]
        public int Id_compra { get; set; }

        public DateTime fecha { get; set; }

        public string producto { get; set; }   

        public int Id_proveedor { get; set; }

        public int Idusuario { get; set; }

        public string Sub_total { get; set; }

        public string total { get; set; }


        //Propiedades de navegacion
        public virtual Proveedor Proveedor { get; set; }

        public virtual Usuario Usuario { get; set; }

        // Propiedad de navegación para la relación uno a muchos
        public virtual ICollection<Detalle_compra> DetallesCompras { get; set; } = new List<Detalle_compra>();
    }
}
