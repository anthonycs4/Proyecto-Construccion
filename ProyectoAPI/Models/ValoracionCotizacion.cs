namespace ProyectoAPI.Models
{
    public class ValoracionCotizacion
    {
        public int Id { get; set; }
        public int Estrellas { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
