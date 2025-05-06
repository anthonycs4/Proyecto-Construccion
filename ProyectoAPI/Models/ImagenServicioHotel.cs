using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_ImagenServicioHotel")]

    public class ImagenServicioHotel
    {
        [Key] // 🚀 MARCA la clave primaria
        [Column("ImagenId")] // 👈 El nombre real de la columna en la base de datos
        public int Id { get; set; }
        public int ServicioHotelId { get; set; }
        public string UrlImagen { get; set; } = string.Empty;

        public ServicioHotel ServicioHotel { get; set; }
    }
}
