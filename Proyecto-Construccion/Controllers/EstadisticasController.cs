using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto_Construccion.Models;
using System.Net.Http.Headers;

public class EstadisticasController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EstadisticasController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IActionResult> Reporte()
    {
        var usuarioJson = HttpContext.Session.GetString("Usuario");
        if (string.IsNullOrEmpty(usuarioJson))
        {
            return RedirectToAction("Usuario", "Login");
        }

        var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
        int usuarioId = usuario.Id;

        try
        {
            Console.WriteLine("ANTES DE LLAMAR A LA API");
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync($"Estadisticas/usuario/{usuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al obtener datos");
            }

            var json = await response.Content.ReadAsStringAsync();
            var datos = JsonConvert.DeserializeObject<List<EstadisticaNegocio>>(json);

            return View(datos);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return View("Error");
        }
    }

}
