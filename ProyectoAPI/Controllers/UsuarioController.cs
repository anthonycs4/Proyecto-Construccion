using Microsoft.AspNetCore.Mvc;
using Proyecto_Backend.Services;
using ProyectoAPI.DTOs;
using System.Threading.Tasks;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AuthService _authService;

        public UsuarioController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("LoginTurista")]
        public async Task<IActionResult> LoginTurista([FromBody] LoginDTO loginDto)
        {
            var usuario = await _authService.AutenticarTurista(loginDto);

            if (usuario == null)
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos, o no eres un turista." });

            return Ok(new
            {
                usuario.Id,
                usuario.Correo,
                usuario.NombresCompleto,
                usuario.TipoUsuarioId
            });
        }

        [HttpPost("LoginDueño")]
        public async Task<IActionResult> LoginDueño([FromBody] LoginDueñoDTO loginDto)
        {
            var usuario = await _authService.AutenticarDueño(loginDto);

            if (usuario == null)
                return Unauthorized(new { mensaje = "RUC o contraseña incorrectos, o no eres un dueño de local." });

            return Ok(new
            {
                usuario.Id,
                usuario.Ruc,
                usuario.NombresCompleto,
                usuario.TipoUsuarioId
            });
        }
    }
}
