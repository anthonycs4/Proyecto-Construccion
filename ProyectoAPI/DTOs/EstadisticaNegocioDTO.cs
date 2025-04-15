namespace ProyectoAPI.DTOs
{
    public class EstadisticaNegocioDTO
    {
        public int NegocioId { get; set; }
        public string NombreNegocio { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int TotalRecomendaciones { get; set; }

        public List<EstadisticaServicioDTO> Servicios { get; set; }
    }

}
