using System.ComponentModel.DataAnnotations;

namespace ExampleAGAPI.Models.DTO
{
    public class AGranelUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int Metros { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Amenidad { get; set; }
        
    }
}
