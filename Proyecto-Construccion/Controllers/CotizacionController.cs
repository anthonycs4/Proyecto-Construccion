using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Proyecto_Construccion.Models;
using System.Net.Http;

namespace Proyecto_Construccion.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly HttpClient _httpClient;

        public CotizacionController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://127.0.0.1:5000/api/"); // Asegúrate de que esta ruta sea correcta
        }

        public ActionResult CotizacionResult()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CustomQuote(
            int dias_viaje,
            decimal presupuesto_usuario,
            string motivo_viaje,
            int cantidad_personas,
            string ubicacion_usuario,
            string fecha_inicio,
            string fecha_fin)
        {
            var requestData = new
            {
                dias_viaje,
                presupuesto_max = presupuesto_usuario,
                motivo_viaje,
                cantidad_personas,
                ubicacion_usuario,
                fecha_inicio,
                fecha_fin
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("generar_cotizaciones", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    if (jsonResponse.Contains("\"error\""))
                    {
                        var errorObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                        if (errorObject != null && errorObject.ContainsKey("error"))
                        {
                            ViewBag.ErrorMessage = $"Error recibido del backend: {errorObject["error"]}";
                            return View("CustomQuote");
                        }
                    }

                    var cotizaciones = JsonConvert.DeserializeObject<List<Cotizacion>>(jsonResponse);
                    return View("CotizacionResult", cotizaciones);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Error al obtener la cotización: {errorContent}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error del servidor: {ex.Message}";
            }

            return View("CustomQuote");
        }

        [HttpPost]
        public async Task<IActionResult> GuardarValoracion([FromBody] ValoracionCotizacion valoracion)
        {
            var json = JsonConvert.SerializeObject(valoracion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // 👉 Cambiado a la URL correcta de tu API ASP.NET
                var response = await _httpClient.PostAsync("http://localhost:5279/api/ValoracionCotizacion", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al enviar la valoración: {ex.Message}");
            }
        }
        public async Task<IActionResult> ObtenerPromedioValoracion(int cotizacionId)
        {
            var response = await _httpClient.GetAsync("http://localhost:5279/api/ValoracionCotizacion/promedio");

            if (response.IsSuccessStatusCode)
            {
                var promedio = await response.Content.ReadAsStringAsync();
                return Json(promedio);
            }

            return Json("No disponible");
        }



    }
}
