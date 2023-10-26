using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ExampleAGAPI.Models
{
    public class NumeroProducto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NroProducto { get; set; }
        [Required]
        public int IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public AGranel Producto { get; set; }
        public string Detalles { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

    }
}
