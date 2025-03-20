using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_Construccion.Models;

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
        public async Task<IActionResult> LoginTurista(string correo, string contraseña)
        {
            var requestData = new { Correo = correo, Contraseña = contraseña };
            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5279/api/Usuario/LoginTurista", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new { mensaje = "Credenciales incorrectas" });
            }

            var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);

            // Guardar sesión del usuario
            HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(usuario));
            HttpContext.Session.SetString("TipoUsuario", usuario.TipoUsuarioId.ToString());

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> LoginDueño(string ruc, string contraseña)
        {
            var requestData = new { Ruc = ruc, Contraseña = contraseña };
            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5279/api/Usuario/LoginDueño", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new { mensaje = "Credenciales incorrectas" });
            }

            var usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);

            // Guardar sesión del usuario
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
        public IActionResult Login()
        {
            return View();
        }
    }
}
