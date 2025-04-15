using Microsoft.AspNetCore.Mvc;
using Proyecto_Backend.Services;
using ProyectoAPI.DTOs;
using ProyectoAPI.Models;
using ProyectoAPI.Services;
using System.Threading.Tasks;

namespace ProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UsuarioService _usuarioService;

        public UsuarioController(AuthService authService, UsuarioService usuarioService)
        {
            _authService = authService;
            _usuarioService = usuarioService;
        }

        [HttpPost("RegistrarCliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody] Usuario dto)
        {
            var exito = await _usuarioService.RegistrarUsuarioCliente(dto);
            if (!exito) return BadRequest(new { mensaje = "No se pudo registrar el cliente." });

            return Ok(new { mensaje = "Cliente registrado correctamente." });
        }

        [HttpPost("RegistrarNegocio")]
        public async Task<IActionResult> RegistrarNegocio([FromBody] Usuario dto)
        {
            var exito = await _usuarioService.RegistrarUsuarioNegocio(dto);
            if (!exito) return BadRequest(new { mensaje = "No se pudo registrar el dueño de negocio. Verifica el RUC." });

            return Ok(new { mensaje = "Dueño de negocio registrado correctamente." });
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
