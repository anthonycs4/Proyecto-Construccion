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
        public IActionResult GetNegocios()
        {
            var negocios = _negocioService.GetNegocios();
            return Ok(negocios);
        }
        [HttpGet("dueno/{usuarioId}")]
        public IActionResult GetNegociosPorDueno(int usuarioId)
        {
            var negocios = _negocioService.GetNegociosPorDueno(usuarioId);
            return Ok(negocios);
        }


    
    [HttpPut("{id}")]
        public IActionResult ActualizarNegocio(int id, [FromBody] NegocioDTO negocioDto)
        {
            var actualizado = _negocioService.ActualizarNegocio(id, negocioDto);
            if (!actualizado) return NotFound();
            return NoContent();
        }
    }
}


