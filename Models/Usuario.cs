using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Usuario
    {
        [Key]
        public int Idusuario {  get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        // Propiedad de navegación inversa para Compras
        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
}
