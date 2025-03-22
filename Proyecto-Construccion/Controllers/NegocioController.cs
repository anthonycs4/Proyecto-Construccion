using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_Construccion.Models;

public class NegocioController : Controller
{
    private readonly HttpClient _httpClient;

    public NegocioController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<IActionResult> Index()
    {
        // Obtener el usuario logueado desde la sesión
        var usuarioJson = HttpContext.Session.GetString("Usuario");
        if (string.IsNullOrEmpty(usuarioJson))
        {
            return RedirectToAction("Usuario","Login");
        }

        var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
        int usuarioId = usuario.Id; // Asegúrate de que 'Id' es la clave correcta


        // Consumir la API para obtener negocios del usuario
        var response = await _httpClient.GetAsync($"negocios/dueno/{usuarioId}");
        if (!response.IsSuccessStatusCode)
        {
            return View(new List<Negocio>()); // Devolver una lista vacía si falla
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var negocios = System.Text.Json.JsonSerializer.Deserialize<List<Negocio>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(negocios);
    }

}
