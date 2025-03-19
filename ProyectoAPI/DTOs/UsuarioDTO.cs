namespace ProyectoAPI.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombresCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string? Ruc { get; set; } // Opcional, solo para dueños
        public int TipoUsuarioId { get; set; }
    }
}
