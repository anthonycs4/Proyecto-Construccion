using Microsoft.AspNetCore.Mvc;
using Proyecto_Construccion.Models;
using System.Text.Json;

namespace Proyecto_Construccion.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("negocios");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Negocio>()); // lista vacía si falla
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var negocios = JsonSerializer.Deserialize<List<Negocio>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(negocios);
        }
    }

}
