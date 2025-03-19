namespace ProyectoAPI.Models
{
    public class Hospedaje
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public string Tipo { get; set; } // Hotel, Hostal, Airbnb, etc.
        public int Capacidad { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public Negocio Negocio { get; set; }
    }
}
