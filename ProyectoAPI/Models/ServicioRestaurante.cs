using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_ServicioRestaurante")] // Aquí especificamos el nombre real de la tabla en SQL Server

    public class ServicioRestaurante
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public string NombrePlato { get; set; }
        public string TipoPlato { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public Negocio Negocio { get; set; }
    }
}
