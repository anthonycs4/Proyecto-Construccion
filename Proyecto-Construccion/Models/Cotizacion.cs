namespace Proyecto_Construccion.Models
{
    public class Cotizacion
    {
        public int HotelId { get; set; }
        public int RestauranteId { get; set; }
        public int LugarId { get; set; }
        public double AjusteEspecial { get; set; }
        public double CostoHotel { get; set; }
        public double CostoLugar { get; set; }
        public double CostoRestaurante { get; set; }
        public string Hotel { get; set; }
        public string Lugar { get; set; }
        public double PorcentajePresupuesto { get; set; }
        public double PresupuestoRestante { get; set; }
        public string Restaurante { get; set; }
        public double Total { get; set; }

    }
}
