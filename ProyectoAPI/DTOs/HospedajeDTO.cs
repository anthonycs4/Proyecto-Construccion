namespace ProyectoAPI.DTOs
{
    public class HospedajeDTO
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public string Tipo { get; set; }
        public int Capacidad { get; set; }
        public decimal PrecioPorNoche { get; set; }
    }
}
