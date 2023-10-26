using System.ComponentModel.DataAnnotations;

namespace ExampleAGAPI.Models.DTO
{
    public class AGranelCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int Metros { get; set; }
        public string ImageUrl { get; set; }
        public string Amenidad { get; set; }
        
    }
}
