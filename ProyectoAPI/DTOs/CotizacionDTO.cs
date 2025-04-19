namespace ProyectoAPI.DTOs
{
    public class CotizacionDTO
    {
        public int CotizacionId { get; set; }  // Asegúrate de que esta propiedad exista

        public int UsuarioId { get; set; }

        public int HotelId { get; set; }
        public string Hotel { get; set; }
        public decimal CostoHotel { get; set; }

        public int RestauranteId { get; set; }
        public string Restaurante { get; set; }
        public decimal CostoRestaurante { get; set; }

        public int LugarId { get; set; }
        public string Lugar { get; set; }
        public decimal CostoLugar { get; set; }

        public decimal Total { get; set; }
        public decimal PresupuestoRestante { get; set; }
        public decimal PorcentajePresupuesto { get; set; }

        public bool AjusteEspecial { get; set; }
    }
}
