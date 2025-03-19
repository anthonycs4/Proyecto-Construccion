using Microsoft.AspNetCore.Mvc;
using Proyecto_Construccion.Models;

namespace Proyecto_Construccion.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Usuario") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
