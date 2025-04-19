using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Models;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionController : ControllerBase
    {
        private readonly CotizacionService _cotizacionService;

        public CotizacionController(CotizacionService cotizacionService)
        {
            _cotizacionService = cotizacionService;
        }

        [HttpPost("guardar")]
        public IActionResult GuardarCotizacion([FromBody] Cotizacion cotizacion)
        {
            // Verificar si el modelo está vacío o no es válido
            if (cotizacion == null || !ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(new { mensaje = "Datos inválidos.", errores = errors });
            }

            try
            {
                // Llamar al servicio para guardar la cotización
                _cotizacionService.GuardarCotizacion(cotizacion);
                return Ok(new { mensaje = "Cotización guardada exitosamente." });
            }
            catch (Exception ex)
            {
                // Manejar cualquier error inesperado
                return StatusCode(500, new { mensaje = "Error al guardar la cotización.", detalles = ex.Message });
            }
        }


        [HttpGet("usuario/{usuarioId}")]
        public IActionResult ObtenerCotizaciones(int usuarioId)
        {
            var cotizaciones = _cotizacionService.ObtenerCotizacionesPorUsuario(usuarioId);
            return Ok(cotizaciones);
        }
    }
}
