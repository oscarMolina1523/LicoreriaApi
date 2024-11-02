using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Proveedor
    {
        [Key]
        public int Id_proveedor {  get; set; }

        public string Direccion { get; set; }

        public string telefono { get; set; }

        public string cedula { get; set; }

        public string Correo { get; set; }

        public bool Estado { get; set; }

        // Propiedad de navegación inversa para Compras
        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();


    }
}
