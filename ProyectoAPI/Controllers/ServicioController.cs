using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.DTOs;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/servicios")]
    public class ServicioController : ControllerBase
    {
        private readonly ServicioService _servicioService;

        public ServicioController(ServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        // 🔹 Obtener servicios de un negocio según su tipo
        [HttpGet("{negocioId}/{tipoServicio}")]
        public IActionResult GetServiciosPorNegocio(int negocioId, string tipoServicio)
        {
            var servicios = _servicioService.GetServiciosPorNegocio(negocioId, tipoServicio);
            if (servicios == null || !servicios.Any()) return NotFound();
            return Ok(servicios);
        }

        // 🔹 Crear un nuevo servicio en un negocio
        [HttpPost("{negocioId}/{tipoServicio}")]
        public IActionResult CrearServicio(int negocioId, string tipoServicio, [FromBody] ServicioDTO servicioDto)
        {
            var creado = _servicioService.CrearServicio(negocioId, tipoServicio, servicioDto);
            if (!creado) return BadRequest("No se pudo crear el servicio.");
            return StatusCode(201, "Servicio creado con éxito.");
        }

        // 🔹 Actualizar un servicio existente
        [HttpPut("{id}/{tipoServicio}")]
        public IActionResult ActualizarServicio(int id, string tipoServicio, [FromBody] ServicioDTO servicioDto)
        {
            var actualizado = _servicioService.ActualizarServicio(id, tipoServicio, servicioDto);
            if (!actualizado) return NotFound("Servicio no encontrado.");
            return NoContent();
        }

        // 🔹 Eliminar un servicio
        [HttpDelete("{id}/{tipoServicio}")]
        public IActionResult EliminarServicio(int id, string tipoServicio)
        {
            var eliminado = _servicioService.EliminarServicio(id, tipoServicio);
            if (!eliminado) return NotFound("Servicio no encontrado.");
            return NoContent();
        }
    }
}
