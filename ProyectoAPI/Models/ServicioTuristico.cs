using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_ServicioTuristico")] // Aquí especificamos el nombre real de la tabla en SQL Server

    public class ServicioTuristico
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public string Provincia { get; set; }
        public string NombreLugar { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public Negocio Negocio { get; set; }
    }
}
