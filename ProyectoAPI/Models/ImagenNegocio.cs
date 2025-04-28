using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Models
{
    [Table("Tb_ImagenNegocio")]
    public class ImagenNegocio
    {
        [Key]
        public int ImagenNegocioId { get; set; }

        [Required]
        public int NegocioId { get; set; } // FK al negocio

        [Required]
        [MaxLength(500)]
        public string UrlImagen { get; set; } = string.Empty;

        [ForeignKey("NegocioId")]
        public Negocio? Negocio { get; set; }
    }
}
