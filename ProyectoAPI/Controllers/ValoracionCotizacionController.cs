using Microsoft.AspNetCore.Mvc;

namespace ProyectoAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProyectoAPI.DTOs;

    [ApiController]
    [Route("api/[controller]")]
    public class ValoracionCotizacionController : ControllerBase
    {
        private readonly ValoracionService _service;

        public ValoracionCotizacionController(ValoracionService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CrearValoracion([FromBody] ValoracionCotizacionDTO dto)
        {
            if (dto.Estrellas < 1 || dto.Estrellas > 5)
                return BadRequest("La valoración debe estar entre 1 y 5 estrellas.");

            _service.GuardarValoracion(dto);
            return Ok(new { mensaje = "Valoración registrada correctamente." });
        }

        [HttpGet]
        public IActionResult ObtenerValoraciones()
        {
            var lista = _service.ObtenerValoraciones();
            return Ok(lista);
        }

        [HttpGet("promedio")]
        public IActionResult ObtenerPromedio()
        {
            var promedio = _service.ObtenerPromedioEstrellas();
            return Ok(new { promedio });
        }
    }

}
