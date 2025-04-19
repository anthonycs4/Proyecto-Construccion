using Microsoft.EntityFrameworkCore;
using ProyectoAPI.Models;

using ProyectoAPI.DTOs;

namespace ProyectoAPI.Services
{
    public class CotizacionService
    {
        private readonly AppDbContext _context;

        public CotizacionService(AppDbContext context)
        {
            _context = context;
        }
        public List<Cotizacion> ObtenerCotizacionesPorUsuario(int usuarioId)
        {
            return _context.Tb_CotizacionGuardada
                .Where(c => c.UsuarioId == usuarioId)
                .OrderByDescending(c => c.FechaGuardado)
                .ToList();
        }
        public void GuardarCotizacion(Cotizacion dto)
        {
            var cotizacion = new Cotizacion
            {
                UsuarioId = dto.UsuarioId,

                HotelId = dto.HotelId,
                Hotel = dto.Hotel,
                CostoHotel = dto.CostoHotel,

                RestauranteId = dto.RestauranteId,
                Restaurante = dto.Restaurante,
                CostoRestaurante = dto.CostoRestaurante,

                LugarId = dto.LugarId,
                Lugar = dto.Lugar,
                CostoLugar = dto.CostoLugar,

                Total = dto.Total,
                PresupuestoRestante = dto.PresupuestoRestante,
                PorcentajePresupuesto = dto.PorcentajePresupuesto,

                AjusteEspecial = dto.AjusteEspecial,
                FechaGuardado = DateTime.Now
            };

            _context.Tb_CotizacionGuardada.Add(cotizacion);
            _context.SaveChanges();
        }
    }

}
