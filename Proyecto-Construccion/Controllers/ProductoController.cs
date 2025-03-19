using Microsoft.AspNetCore.Mvc;
using Proyecto_Construccion.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProyectoFront.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            //var response = await _httpClient.GetStringAsync("http://localhost:5279/api/productos");
            //var productos = JsonConvert.DeserializeObject<List<Producto>>(response);
            return View();
        }
    }
}
