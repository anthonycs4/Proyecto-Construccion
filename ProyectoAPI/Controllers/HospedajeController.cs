using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.DTOs;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/hospedajes")]
    public class HospedajeController : ControllerBase
    {
        private readonly HospedajeService _hospedajeService;

        public HospedajeController(HospedajeService hospedajeService)
        {
            _hospedajeService = hospedajeService;
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarHospedaje(int id, [FromBody] HospedajeDTO hospedajeDto)
        {
            var actualizado = _hospedajeService.ActualizarHospedaje(id, hospedajeDto);
            if (!actualizado) return NotFound();
            return NoContent();
        }
    }
}
