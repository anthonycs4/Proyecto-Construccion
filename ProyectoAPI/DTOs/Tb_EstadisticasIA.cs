using ProyectoAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Tb_EstadisticasIA")]
public class Tb_EstadisticasIA
{
    [Key]
    public int Id { get; set; }

    public int NegocioId { get; set; } // 🔗 La FK que causa conflicto

    // Agrega aquí las demás propiedades que tenga tu tabla
    // Por ejemplo:
    // public int CantidadRecomendaciones { get; set; }
    // public DateTime FechaRegistro { get; set; }

    // Relación de navegación (opcional pero útil)
    [ForeignKey("NegocioId")]
    public Negocio Negocio { get; set; }
}
