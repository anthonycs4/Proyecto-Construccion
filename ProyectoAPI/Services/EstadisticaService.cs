using Microsoft.EntityFrameworkCore;
using ProyectoAPI.DTOs;
using ProyectoAPI.Models;

namespace ProyectoAPI.Services
{
    public class EstadisticaService
    {
        private readonly AppDbContext _context;

        public EstadisticaService(AppDbContext context)
        {
            _context = context;
        }

        public List<EstadisticaNegocioDTO> ObtenerEstadisticasPorUsuario(int usuarioId)
        {
            var negocios = _context.Negocios
                .Where(n => n.UsuarioId == usuarioId)
                .ToList();

            var estadisticas = _context.Tb_EstadisticasRecomendacion.ToList();

            var resultado = new List<EstadisticaNegocioDTO>();

            foreach (var negocio in negocios)
            {
                var estadisticasNegocio = estadisticas
                    .Where(e => e.NegocioId == negocio.Id)
                    .ToList();

                var total = estadisticasNegocio.Count;

                var servicios = estadisticasNegocio
                    .GroupBy(e => new { e.ServicioId, e.TipoServicio })
                    .Select(g => new EstadisticaServicioDTO
                    {
                        ServicioId = g.Key.ServicioId,
                        TipoServicio = g.Key.TipoServicio,
                        VecesRecomendado = g.Count(),
                        Nombre = null, // Puedes completar esto si gustas con los datos reales
                        Precio = 0
                    })
                    .OrderByDescending(s => s.VecesRecomendado)
                    .ToList();

                resultado.Add(new EstadisticaNegocioDTO
                {
                    NegocioId = negocio.Id,
                    NombreNegocio = negocio.Nombre,
                    Direccion = negocio.Direccion,
                    Telefono = negocio.Telefono,
                    TotalRecomendaciones = total,
                    Servicios = servicios
                });
            }

            return resultado;
        }
    }

}
