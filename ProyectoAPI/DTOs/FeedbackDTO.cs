namespace ProyectoAPI.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public int UsuarioId { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
