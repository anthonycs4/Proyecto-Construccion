namespace ProyectoAPI.DTOs
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        public int NegocioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
