using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Construccion.Controllers
{
    public class TestController : Controller
    {
        public IActionResult VerificarSesion()
        {
            var usuarioJson = HttpContext.Session.GetString("Usuario");
            return Content(usuarioJson ?? "No hay sesión guardada.");
        }
    }
}
