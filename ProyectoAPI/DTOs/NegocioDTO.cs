namespace ProyectoAPI.DTOs
{
    public class NegocioDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int TipoNegocioId { get; set; }
        public int ProvinciaId { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string? Descripcion { get; set; }
    }

}
