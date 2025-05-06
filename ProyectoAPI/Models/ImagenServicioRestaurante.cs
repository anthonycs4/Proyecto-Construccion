using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_ImagenServicioRestaurante")]

    public class ImagenServicioRestaurante
    {
        [Key] // 🚀 MARCA la clave primaria
        [Column("ImagenId")] // 👈 El nombre real de la columna en la base de datos
        public int Id { get; set; }
        public int ServicioRestauranteId { get; set; }
        public string UrlImagen { get; set; } = string.Empty;

        public ServicioRestaurante ServicioRestaurante { get; set; }
    }
}
