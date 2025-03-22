using System.Net.Http;
using System.Text.Json;
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
        // Llamada a la API
        var response = await _httpClient.GetAsync($"servicios/{negocioId}/{tipoServicio}");
        if (!response.IsSuccessStatusCode)
        {
            return View(new List<object>()); // Retorna una lista vacía en caso de error
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        dynamic servicios = tipoServicio switch
        {
            "Hotel" => JsonConvert.DeserializeObject<List<Servicio>>(jsonResponse),
            "Restaurante" => JsonConvert.DeserializeObject<List<Servicio>>(jsonResponse),
            "Turismo" => JsonConvert.DeserializeObject<List<Servicio>>(jsonResponse),
            _ => new List<object>()
        };

        ViewBag.TipoServicio = tipoServicio;
        return View(servicios);
    }
}
