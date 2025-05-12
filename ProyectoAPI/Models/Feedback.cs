using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_Feedback")] // <- Esto lo enlaza con tu tabla real

    public class Feedback
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public int UsuarioId { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
        public DateTime Fecha { get; set; }
    }

}
