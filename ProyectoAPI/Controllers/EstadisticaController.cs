using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.DTOs;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadisticasController : ControllerBase
    {
        private readonly EstadisticaService _estadisticaService;

        public EstadisticasController(EstadisticaService estadisticaService)
        {
            _estadisticaService = estadisticaService;
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult GetEstadisticasPorUsuario(int usuarioId)
        {
            var resultado = _estadisticaService.ObtenerEstadisticasPorUsuario(usuarioId);
            return Ok(resultado);
        }
    }


}
