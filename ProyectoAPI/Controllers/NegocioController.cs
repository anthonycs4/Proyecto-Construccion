using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.DTOs;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/negocios")]
    public class NegocioController : ControllerBase
    {
        private readonly NegocioService _negocioService;

        public NegocioController(NegocioService negocioService)
        {
            _negocioService = negocioService;
        }

        [HttpGet]
        public IActionResult GetNegocios() => Ok(_negocioService.GetNegocios());

        [HttpGet("dueno/{usuarioId}")]
        public IActionResult GetNegociosPorDueno(int usuarioId) => Ok(_negocioService.GetNegociosPorDueno(usuarioId));
        [HttpPost("dueno/{usuarioId}")]
        public IActionResult RegistrarNegocio(int usuarioId, [FromBody] NegocioDTO negocioDto)
        {
            if (negocioDto == null) return BadRequest("El negocio no puede ser nulo.");

            var nuevoNegocio = _negocioService.RegistrarNegocio(usuarioId, negocioDto);

            return nuevoNegocio != null
                ? CreatedAtAction(nameof(GetNegociosPorDueno), new { usuarioId }, nuevoNegocio)
                : BadRequest("No se pudo registrar el negocio.");
        }


        [HttpPut("{idNegocio}/dueno/{idUsuario}")]
        public IActionResult ActualizarNegocio(int idNegocio, int idUsuario, [FromBody] NegocioDTO negocioDto)
        {
            var actualizado = _negocioService.ActualizarNegocio(idNegocio, idUsuario, negocioDto);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarNegocio(int id) =>
            _negocioService.EliminarNegocio(id) ? NoContent() : NotFound();
    }
}
