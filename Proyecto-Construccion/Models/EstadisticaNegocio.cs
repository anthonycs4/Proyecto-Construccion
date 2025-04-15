namespace Proyecto_Construccion.Models
{
    public class EstadisticaNegocio
    {
        public int NegocioId { get; set; }
        public string? NombreNegocio { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int TotalRecomendaciones { get; set; }
        public List<EstadisticaServicio> Servicios { get; set; } = new();
    }
}
