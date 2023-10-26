using System.ComponentModel.DataAnnotations;

namespace ExampleAGAPI.Models.DTO
{
    public class NumeroProductoUpdateDTO
    {
        [Required]
        public int NroProducto { get; set; }
        [Required]
        public int IdProducto { get; set; }
        public string Detalles { get; set; }
    }
}
