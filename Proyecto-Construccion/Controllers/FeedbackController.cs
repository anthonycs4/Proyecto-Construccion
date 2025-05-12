using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_Construccion.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class FeedbackController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FeedbackController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("feedback/crear/{negocioId}")]
    public async Task<IActionResult> Crear(int negocioId)
    {
        // Obtener usuario desde sesión
        var usuarioJson = HttpContext.Session.GetString("Usuario");
        if (string.IsNullOrEmpty(usuarioJson))
            return RedirectToAction("Login", "Usuario");

        var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);

        var client = _httpClientFactory.CreateClient("ApiClient");

        var response = await client.GetAsync($"negocios/{negocioId}");
        if (!response.IsSuccessStatusCode)
            return View("Error");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var negocio = JsonSerializer.Deserialize<Negocio>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (negocio == null)
            return NotFound();

        var model = new Feedback
        {
            NegocioId = negocio.Id,
            UsuarioId = usuario.Id
        };

        return View(model);
    }

    [HttpPost("feedback/crear")]
    public async Task<IActionResult> Crear([FromBody] Feedback model)
    {
        Console.WriteLine("📩 JSON recibido:");
        Console.WriteLine(JsonConvert.SerializeObject(model));

        if (!ModelState.IsValid)
        {
            Console.WriteLine("❌ Modelo inválido");
            return BadRequest(ModelState);
        }

        var feedback = new
        {
            id = 0,
            negocioId = model.NegocioId,
            usuarioId = model.UsuarioId,
            comentario = model.Comentario,
            calificacion = model.Calificacion,
            fecha = DateTime.UtcNow
        };

        var client = _httpClientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync("feedback", feedback);

        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine("📤 Respuesta de API:");
        Console.WriteLine(responseContent);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        return StatusCode((int)response.StatusCode, "Error al guardar feedback");
    }
    [HttpGet("feedback/ver/{negocioId}")]
    public async Task<IActionResult> VerFeedbacks(int negocioId)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");

        var response = await client.GetAsync($"feedback/negocio/{negocioId}");
        if (!response.IsSuccessStatusCode)
            return View("Error");

        var content = await response.Content.ReadAsStringAsync();
        var feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(content);

        ViewBag.NegocioId = negocioId; // por si quieres mostrar el ID o usarlo luego
        return View(feedbacks);
    }


}
