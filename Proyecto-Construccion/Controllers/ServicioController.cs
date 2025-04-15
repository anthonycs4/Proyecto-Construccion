using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_Construccion.Models;
using System.Collections.Generic;

public class ServicioController : Controller
{
    private readonly HttpClient _httpClient;

    public ServicioController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<IActionResult> Servicios(int negocioId, string tipoServicio)
    {
        var response = await _httpClient.GetAsync($"servicios/{negocioId}/{tipoServicio}");
        if (!response.IsSuccessStatusCode)
        {
            return View(new List<object>());
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic servicios = JsonConvert.DeserializeObject<List<Servicio>>(jsonResponse);

        ViewBag.TipoServicio = tipoServicio;
        ViewBag.NegocioId = negocioId;
        return View(servicios);
    }
    [HttpGet]
    public IActionResult Crear(int negocioId, string tipoServicio)
    {
        if (negocioId == 0)
        {
            return BadRequest("El negocioId es requerido.");
        }

        ViewBag.TipoServicio = tipoServicio;
        ViewBag.NegocioId = negocioId;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Servicio servicio)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _httpClient.PostAsJsonAsync($"servicios/{servicio.NegocioId}/{servicio.TipoServicio}", servicio);

        if (response.IsSuccessStatusCode)
        {
            return Ok(new { message = "Servicio creado exitosamente" });
        }
        else
        {
            return StatusCode((int)response.StatusCode, "Error al crear el servicio.");
        }
    }



    public async Task<IActionResult> Editar(int id, string tipoServicio)
    {
        var response = await _httpClient.GetAsync($"servicios/detalle/{id}/{tipoServicio}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound();
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var servicio = JsonConvert.DeserializeObject<Servicio>(jsonResponse);

        ViewBag.TipoServicio = tipoServicio;
        return View(servicio);
    }

    [HttpPost]
    public async Task<IActionResult> Editar([FromBody] Servicio servicio)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var jsonData = JsonConvert.SerializeObject(servicio);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"servicios/{servicio.Id}/{servicio.TipoServicio}", content);
        if (response.IsSuccessStatusCode)
        {
            return Ok(new { message = "Servicio actualizado correctamente." });
        }

        return StatusCode((int)response.StatusCode, "Ocurrió un error al actualizar el servicio.");
    }


    [HttpPost]
    public async Task<IActionResult> Eliminar(int id, int negocioId, string tipoServicio)
    {
        var response = await _httpClient.DeleteAsync($"servicios/{id}/{tipoServicio}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Servicios", new { negocioId, tipoServicio });
        }

        ModelState.AddModelError("", "Error al eliminar el servicio");
        return RedirectToAction("Servicios", new { negocioId, tipoServicio });
    }
}
