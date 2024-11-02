using System.ComponentModel.DataAnnotations;

namespace LicoreriaBackend.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }
    }
}
