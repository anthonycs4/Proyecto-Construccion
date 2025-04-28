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
    public IActionResult Crear()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Crear(string Nombre, int TipoNegocioId, string Direccion, string Telefono, string Descripcion, List<IFormFile> Imagenes)
    {
        Console.WriteLine("==> Iniciando método Crear");

        var usuarioJson = HttpContext.Session.GetString("Usuario");
        if (string.IsNullOrEmpty(usuarioJson))
        {
            Console.WriteLine("==> Usuario no encontrado en sesión");
            TempData["Error"] = "Debes iniciar sesión.";
            return RedirectToAction("Login", "Account");
        }

        var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
        if (usuario == null)
        {
            Console.WriteLine("==> Error al deserializar usuario de sesión");
            TempData["Error"] = "No se pudo obtener los datos del usuario.";
            return RedirectToAction("Login", "Account");
        }

        Console.WriteLine($"==> Usuario obtenido: ID={usuario.Id}");

        // Guardar las imágenes en wwwroot/imagenes/negocios
        var imagenesRutas = new List<string>();
        if (Imagenes != null && Imagenes.Count > 0)
        {
            Console.WriteLine($"==> Se recibieron {Imagenes.Count} imágenes para subir.");

            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "negocios");

            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
                Console.WriteLine($"==> Carpeta creada: {rutaCarpeta}");
            }

            foreach (var imagen in Imagenes)
            {
                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName); // Nombre único
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                var rutaRelativa = $"/imagenes/negocios/{nombreArchivo}";
                imagenesRutas.Add(rutaRelativa);
                Console.WriteLine($"==> Imagen guardada: {rutaRelativa}");
            }
        }
        else
        {
            Console.WriteLine("==> No se recibieron imágenes para subir.");
        }

        var nuevoNegocio = new
        {
            id = 0,
            usuarioId = usuario.Id,
            nombre = Nombre,
            tipoNegocioId = TipoNegocioId,
            provinciaId = 1, // Siempre provincia 1
            direccion = Direccion,
            telefono = Telefono,
            descripcion = Descripcion,
            imagenes = imagenesRutas
        };

        var nuevoNegocioJson = JsonConvert.SerializeObject(nuevoNegocio);
        Console.WriteLine("==> JSON enviado al backend:");
        Console.WriteLine(nuevoNegocioJson);

        var content = new StringContent(nuevoNegocioJson, System.Text.Encoding.UTF8, "application/json");

        var url = $"negocios/dueno/{usuario.Id}";
        Console.WriteLine($"==> URL de POST: {url}");

        var response = await _httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("==> Negocio creado exitosamente.");
            TempData["Success"] = "Negocio creado exitosamente.";
            return RedirectToAction("Index");
        }
        else
        {
            Console.WriteLine($"==> Error al crear el negocio. StatusCode: {response.StatusCode}");
            TempData["Error"] = "Ocurrió un error al crear el negocio.";
            return View();
        }
    }

    public async Task<IActionResult> Detalle(int id)
    {

        
        // Consumir la API para obtener detalles del negocio
        var response = await _httpClient.GetAsync($"negocios/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return View("Error"); // Si la API falla, mostramos una página de error
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var negocio = System.Text.Json.JsonSerializer.Deserialize<Negocio>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(negocio);
    }


}
