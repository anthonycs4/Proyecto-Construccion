using Microsoft.AspNetCore.Mvc;
using Proyecto_Construccion.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class NegocioController : Controller
{
    private readonly HttpClient _httpClient;

    public NegocioController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    // Obtener todos los negocios
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("negocios");
        if (!response.IsSuccessStatusCode) return View(new List<Negocio>());

        var json = await response.Content.ReadAsStringAsync();
        var negocios = JsonSerializer.Deserialize<List<Negocio>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(negocios);
    }

    // Obtener negocios por usuario
    public async Task<IActionResult> NegociosPorUsuario(int usuarioId)
    {
        var response = await _httpClient.GetAsync($"negocios/dueno/{usuarioId}");
        if (!response.IsSuccessStatusCode) return View(new List<Negocio>());

        var json = await response.Content.ReadAsStringAsync();
        var negocios = JsonSerializer.Deserialize<List<Negocio>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View("Index", negocios);
    }

    // Crear un negocio
    [HttpPost]
    public async Task<IActionResult> Crear(Negocio negocio, int usuarioId)
    {
        var content = new StringContent(JsonSerializer.Serialize(negocio), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"negocios/dueno/{usuarioId}", content);

        if (!response.IsSuccessStatusCode) return BadRequest("No se pudo crear el negocio.");
        return RedirectToAction("Index");
    }

    // Editar un negocio
    [HttpPost]
    public async Task<IActionResult> Editar(int idNegocio, Negocio negocio, int usuarioId)
    {
        var content = new StringContent(JsonSerializer.Serialize(negocio), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"negocios/{idNegocio}/dueno/{usuarioId}", content);

        if (!response.IsSuccessStatusCode) return BadRequest("No se pudo actualizar el negocio.");
        return RedirectToAction("Index");
    }

    // Eliminar un negocio
    [HttpPost]
    public async Task<IActionResult> Eliminar(int idNegocio)
    {
        var response = await _httpClient.DeleteAsync($"negocios/{idNegocio}");
        if (!response.IsSuccessStatusCode) return BadRequest("No se pudo eliminar el negocio.");
        return RedirectToAction("Index");
    }
}
