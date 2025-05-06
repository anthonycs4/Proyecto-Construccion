using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_ServicioHotel")] // Aquí especificamos el nombre real de la tabla en SQL Server

    public class ServicioHotel
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public int CantidadPersonas { get; set; }
        public bool WiFi { get; set; }
        public bool AguaCaliente { get; set; }
        public bool RoomService { get; set; }
        public bool Cochera { get; set; }
        public bool Cable { get; set; }
        public bool DesayunoIncluido { get; set; }
        public decimal Precio { get; set; }
        public string Estado { get; set; }
        public Negocio Negocio { get; set; }
        public List<ImagenServicioHotel> Imagenes { get; set; } = new(); // Navegación

    }
}
