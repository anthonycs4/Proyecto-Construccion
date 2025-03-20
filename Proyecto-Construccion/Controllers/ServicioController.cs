using Microsoft.AspNetCore.Mvc;
using Proyecto_Construccion.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ServicioController : Controller
{
    private readonly HttpClient _httpClient;

    public ServicioController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    // Obtener todos los servicios de un negocio
    public async Task<IActionResult> ServiciosPorNegocio(int negocioId)
    {
        var response = await _httpClient.GetAsync($"servicios/negocio/{negocioId}");
        if (!response.IsSuccessStatusCode) return View(new List<Servicio>());

        var json = await response.Content.ReadAsStringAsync();
        var servicios = JsonSerializer.Deserialize<List<Servicio>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View("Index", servicios);
    }

    // Crear un servicio
    [HttpPost]
    public async Task<IActionResult> Crear(Servicio servicio, int negocioId)
    {
        var content = new StringContent(JsonSerializer.Serialize(servicio), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"servicios/negocio/{negocioId}", content);

        if (!response.IsSuccessStatusCode) return BadRequest("No se pudo crear el servicio.");
        return RedirectToAction("ServiciosPorNegocio", new { negocioId });
    }

    // Editar un servicio
    [HttpPost]
    public async Task<IActionResult> Editar(int servicioId, Servicio servicio, int negocioId)
    {
        var content = new StringContent(JsonSerializer.Serialize(servicio), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"servicios/{servicioId}/negocio/{negocioId}", content);

        if (!response.IsSuccessStatusCode) return BadRequest("No se pudo actualizar el servicio.");
        return RedirectToAction("ServiciosPorNegocio", new { negocioId });
    }

    // Eliminar un servicio
    [HttpPost]
    public async Task<IActionResult> Eliminar(int servicioId, int negocioId)
    {
        var response = await _httpClient.DeleteAsync($"servicios/{servicioId}");
        if (!response.IsSuccessStatusCode) return BadRequest("No se pudo eliminar el servicio.");
        return RedirectToAction("ServiciosPorNegocio", new { negocioId });
    }
}
