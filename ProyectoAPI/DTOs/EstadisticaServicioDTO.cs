namespace ProyectoAPI.DTOs
{
    public class EstadisticaServicioDTO
    {
        public int ServicioId { get; set; }
        public string TipoServicio { get; set; }
        public int VecesRecomendado { get; set; }

        // Detalles del servicio
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

}
