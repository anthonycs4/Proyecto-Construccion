﻿namespace ProyectoAPI.DTOs
{
    public class ServicioDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }

        // Propiedades específicas para Servicios de Hotel
        public int CantidadPersonas { get; set; }
        public bool WiFi { get; set; }
        public bool AguaCaliente { get; set; }
        public bool RoomService { get; set; }
        public bool Cochera { get; set; }
        public bool Cable { get; set; }
        public bool DesayunoIncluido { get; set; }

        // Propiedades específicas para Servicios de Restaurante
        public string? TipoPlato { get; set; }
        public string? Descripcion { get; set; }

        // Propiedades específicas para Servicios Turísticos
        public string? Provincia { get; set; }
        public List<string> Fotos { get; set; }

    }
}
