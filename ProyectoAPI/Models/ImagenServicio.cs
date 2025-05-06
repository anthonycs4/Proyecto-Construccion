using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Models
{
    [Table("Tb_ImagenServicio")]
    public class ImagenServicio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ServicioId { get; set; } // FK al servicio

        [Required]
        [MaxLength(500)]
        public string UrlImagen { get; set; } = string.Empty;

        [ForeignKey("ServicioId")]
        public Servicio? Servicio { get; set; }
    }



}
