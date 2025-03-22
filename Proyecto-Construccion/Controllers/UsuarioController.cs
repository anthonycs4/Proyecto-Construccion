using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_Construccion.Models;
 // Importa los DTOs

namespace Proyecto_Construccion.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> LoginTurista([FromBody] Usuario loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Correo) || string.IsNullOrEmpty(loginDto.Contraseña))
                return BadRequest(new { mensaje = "Datos incompletos" });

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5279/api/Usuario/LoginTurista", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { mensaje = "Credenciales incorrectas" });

            var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);
            if (usuario == null)
                return BadRequest(new { mensaje = "No se pudo obtener el usuario" });

            // Guardar datos en sesión
            HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));
            HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuarioId.ToString());

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> LoginDueño([FromBody] Usuario loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Ruc) || string.IsNullOrEmpty(loginDto.Contraseña))
                return BadRequest(new { mensaje = "Datos incompletos" });

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5279/api/Usuario/LoginDueño", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { mensaje = "Credenciales incorrectas" });

            var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);
            if (usuario == null)
                return BadRequest(new { mensaje = "No se pudo obtener el usuario" });

            // Guardar datos en sesión
            HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));
            HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuarioId.ToString());

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { mensaje = "Sesión cerrada" });
        }
        [HttpGet]
        public IActionResult ObtenerSesion()
        {
            var usuario = HttpContext.Session.GetString("Usuario");
            var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return BadRequest(new { mensaje = "No hay sesión activa" });
            }

            return Ok(new { Usuario = usuario, TipoUsuario = tipoUsuario });
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
