using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_Construccion.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json;


/// <summary>
/// RF02 - Proporcionar a los dueños la capacidad de actualizar información de negocios en tiempo real,
/// actualmente implementado solo para servicios de hospedaje.
/// Controlador para gestionar los servicios asociados a los negocios del usuario.
/// </summary>
public class ServicioController : Controller
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// RF02 - Inicializa el controlador con un cliente HTTP para consumir la API de servicios.
    /// </summary>
    public ServicioController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    /// <summary>
    /// RF02 - Muestra la lista de servicios de un negocio según su tipo (actualmente aplicable a hospedaje).
    /// </summary>
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

    /// <summary>
    /// RF02 - Vista para crear un nuevo servicio dentro del negocio, considerando el tipo hospedaje.
    /// </summary>
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

    /// <summary>
    /// RF02 - Envía los datos del nuevo servicio a la API, incluyendo las imágenes cargadas.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Crear(IFormCollection form, List<IFormFile> Imagenes)
    {
        var servicio = new Servicio
        {
            Id = 0,
            NegocioId = int.Parse(form["NegocioId"]),
            TipoServicio = form["TipoServicio"],
            Precio = decimal.Parse(form["Precio"]),
            Nombre = form["Nombre"],
            TipoPlato = form["TipoPlato"],
            Descripcion = form["Descripcion"],
            Provincia = form["Provincia"],
            CantidadPersonas = int.TryParse(form["CantidadPersonas"], out var cp) ? cp : 0,
            WiFi = form["WiFi"] == "on",
            AguaCaliente = form["AguaCaliente"] == "on",
            RoomService = form["RoomService"] == "on",
            Cochera = form["Cochera"] == "on",
            Cable = form["Cable"] == "on",
            DesayunoIncluido = form["DesayunoIncluido"] == "on"
        };

        var imagenesRutas = new List<string>();
        if (Imagenes != null && Imagenes.Count > 0)
        {
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "servicios");
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            foreach (var imagen in Imagenes)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    await imagen.CopyToAsync(stream);

                var rutaRelativa = $"/imagenes/servicios/{nombreArchivo}";
                imagenesRutas.Add(rutaRelativa);
            }
        }

        servicio.fotos = imagenesRutas;

        var response = await _httpClient.PostAsJsonAsync($"servicios/{servicio.NegocioId}/{servicio.TipoServicio}", servicio);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }

        return StatusCode((int)response.StatusCode, "Error al crear el servicio.");
    }

    /// <summary>
    /// RF02 - Obtiene los datos actuales de un servicio para mostrarlos en el formulario de edición.
    /// </summary>
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

    /// <summary>
    /// RF02 - Permite actualizar los datos de un servicio en tiempo real, incluyendo imágenes nuevas.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Editar([FromForm] Servicio servicio, IList<IFormFile> Imagenes)
    {
        var imagenesRutas = new List<string>();
        if (Imagenes != null && Imagenes.Count > 0)
        {
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "servicios");
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            foreach (var imagen in Imagenes)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    await imagen.CopyToAsync(stream);

                var rutaRelativa = $"/imagenes/servicios/{nombreArchivo}";
                imagenesRutas.Add(rutaRelativa);
            }
        }

        servicio.fotos = imagenesRutas;

        var json = System.Text.Json.JsonSerializer.Serialize(servicio, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine("JSON enviado a la API:");
        Console.WriteLine(json);

        var url = $"servicios/{servicio.NegocioId}/{servicio.TipoServicio}";
        Console.WriteLine("URL a la que se enviará la solicitud:");
        Console.WriteLine(url);

        try
        {
            var response = await _httpClient.PutAsJsonAsync(url, servicio);

            Console.WriteLine("Código de estado de la respuesta: " + response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Contenido de la respuesta:");
            Console.WriteLine(responseContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return StatusCode((int)response.StatusCode, responseContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al hacer la solicitud a la API:");
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Error interno al conectar con la API.");
        }
    }

    /// <summary>
    /// RF02 - Elimina un servicio asociado a un negocio según su tipo, aplicable a hospedaje.
    /// </summary>
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
