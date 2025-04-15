namespace Proyecto_Construccion.Models
{
    public class EstadisticaServicio
    {
        public int ServicioId { get; set; }
        public string TipoServicio { get; set; }
        public int VecesRecomendado { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
