namespace Proyecto_Construccion.Models
{
    public class Feedback
    {
        public int NegocioId { get; set; }
        public int UsuarioId { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; } // De 0 a 5
        public DateTime Fecha { get; set; }
    }
}
