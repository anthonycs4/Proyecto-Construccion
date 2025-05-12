using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.DTOs;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeedbackDTO dto)
        {
            var created = await _feedbackService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByUsuarioId), new { usuarioId = created.UsuarioId }, created);
        }


        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuarioId(int usuarioId)
        {
            var result = await _feedbackService.GetByUsuarioIdAsync(usuarioId);
            return Ok(result);
        }

        [HttpGet("negocio/{negocioId}")]
        public async Task<IActionResult> ObtenerFeedbacksPorNegocio(int negocioId)
        {
            var feedbacks = await _feedbackService.GetFeedbacksPorNegocio(negocioId);

            if (feedbacks == null || !feedbacks.Any())
            {
                return NotFound("No se encontraron feedbacks para este negocio.");
            }

            return Ok(feedbacks);
        }
    }

}
