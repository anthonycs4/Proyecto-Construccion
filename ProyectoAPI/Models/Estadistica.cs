namespace ProyectoAPI.Models
{
    public class Estadistica
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public int ServicioId { get; set; }
        public string TipoServicio { get; set; }
        public DateTime FechaRecomendacion { get; set; }
    }
}
