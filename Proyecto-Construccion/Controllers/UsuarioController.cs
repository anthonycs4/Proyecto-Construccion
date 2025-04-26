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

            var response = await _httpClient.PostAsync("https://app-back-valverde-cano.azurewebsites.net/api/Usuario/LoginTurista", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { mensaje = "Credenciales incorrectas" });

            var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);
            if (usuario == null)
                return BadRequest(new { mensaje = "No se pudo obtener el usuario" });

            // Guardar datos en sesión
            HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));
            HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuarioId.ToString());
            // Al momento de iniciar sesión y obtener el objeto usuario:
            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            HttpContext.Session.SetString("UsuarioNombre", usuario.NombresCompleto);
            HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);
            HttpContext.Session.SetString("UsuarioTipo", usuario.TipoUsuarioId.ToString());

            var options = new CookieOptions
            {
                Secure = true, // Solo se enviará por HTTPS
                SameSite = SameSiteMode.Strict, // Para prevenir CSRF
                Expires = DateTimeOffset.UtcNow.AddDays(7) // Duración de la cookie
            };

            // Supón que tienes un objeto `usuario` con las propiedades necesarias
            Response.Cookies.Append("UsuarioId", usuario.Id.ToString(), options);
            Response.Cookies.Append("UsuarioNombre", usuario.NombresCompleto, options);
            Response.Cookies.Append("UsuarioCorreo", usuario.Correo, options);
            Response.Cookies.Append("UsuarioTipo", usuario.TipoUsuarioId.ToString(), options);


            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> LoginDueño([FromBody] Usuario loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Ruc) || string.IsNullOrEmpty(loginDto.Contraseña))
                return BadRequest(new { mensaje = "Datos incompletos" });

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://app-back-valverde-cano.azurewebsites.net/api/Usuario/LoginDueño", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { mensaje = "Credenciales incorrectas" });

            var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);
            if (usuario == null)
                return BadRequest(new { mensaje = "No se pudo obtener el usuario" });

            // Guardar datos en sesión
            HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));
            HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuarioId.ToString());
            var options = new CookieOptions
            {
                Secure = true, // Solo se enviará por HTTPS
                SameSite = SameSiteMode.Strict, // Para prevenir CSRF
                Expires = DateTimeOffset.UtcNow.AddDays(7) // Duración de la cookie
            };

            // Supón que tienes un objeto `usuario` con las propiedades necesarias
            Response.Cookies.Append("UsuarioId", usuario.Id.ToString(), options);
            Response.Cookies.Append("UsuarioNombre", usuario.NombresCompleto, options);
            Response.Cookies.Append("UsuarioCorreo", usuario.Correo, options);
            Response.Cookies.Append("UsuarioTipo", usuario.TipoUsuarioId.ToString(), options);


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
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegistrarCliente([FromBody] Usuario nuevoUsuario)
        {
            var content = new StringContent(JsonConvert.SerializeObject(nuevoUsuario), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://app-back-valverde-cano.azurewebsites.net/api/Usuario/RegistrarCliente", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { mensaje = "Error al registrar cliente" });

            return Ok(new { mensaje = "Cliente registrado correctamente" });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarDueño([FromBody] Usuario nuevoUsuario)
        {
            var content = new StringContent(JsonConvert.SerializeObject(nuevoUsuario), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://app-back-valverde-cano.azurewebsites.net/api/Usuario/RegistrarNegocio", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { mensaje = "Error al registrar dueño" });

            return Ok(new { mensaje = "Dueño registrado correctamente" });
        }

    }
}
