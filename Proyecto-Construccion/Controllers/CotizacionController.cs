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
        public async Task<IActionResult> GuardarCotizacion([FromBody] Cotizacion cotizacion)
        {
            // Log del inicio del proceso
            Console.WriteLine($"Intentando guardar cotización: {JsonConvert.SerializeObject(cotizacion)}");

            // Validar sesión
            var usuarioJson = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(usuarioJson))
            {
                Console.WriteLine("Sesión no válida.");
                TempData["Error"] = "Debes iniciar sesión para guardar una cotización.";
                return Json(new { success = false, message = "Debes iniciar sesión para guardar una cotización." });
            }

            // Obtener usuario
            var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);

            if (usuario == null)
            {
                Console.WriteLine("Error al deserializar el objeto Usuario.");
                TempData["Error"] = "No se pudo obtener los datos del usuario.";
                return Json(new { success = false, message = "No se pudo obtener los datos del usuario." });
            }

            // Validación de datos
            if (cotizacion == null)
            {
                Console.WriteLine("La cotización es null.");
                TempData["Error"] = "La cotización no se pudo procesar, ya que el objeto es nulo.";
                return Json(new { success = false, message = "La cotización no se pudo procesar, ya que el objeto es nulo." });
            }

            // Ahora puedes realizar la validación de los campos
            if (cotizacion.Total <= 0 || cotizacion.HotelId == 0 || cotizacion.RestauranteId == 0 || cotizacion.LugarId == 0)
            {
                Console.WriteLine("Datos de la cotización incompletos.");
                string detalleError = $"Datos de la cotización incompletos. " +
                                      $"HotelId: {cotizacion.HotelId}, " +
                                      $"RestauranteId: {cotizacion.RestauranteId}, " +
                                      $"LugarId: {cotizacion.LugarId}, " +
                                      $"Total: {cotizacion.Total}";

                Console.WriteLine(detalleError);
                TempData["Error"] = detalleError;

                return Json(new { success = false, message = detalleError });
            }


            try
            {
                var json = JsonConvert.SerializeObject(cotizacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Log antes de hacer la petición a la API
                Console.WriteLine("Enviando datos al API...");
                var response = await _httpClient.PostAsync("http://localhost:5279/api/Cotizacion/guardar", content);

                // Log de la respuesta
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Cotización guardada exitosamente.");
                    TempData["Mensaje"] = "Cotización guardada exitosamente.";
                    return Json(new { success = true, message = "Cotización guardada exitosamente." });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al guardar cotización: {errorContent}");
                    TempData["Error"] = $"Error al guardar: {errorContent}";
                    return Json(new { success = false, message = errorContent });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error del servidor: {ex.Message}");
                TempData["Error"] = $"Error del servidor: {ex.Message}";
                return Json(new { success = false, message = $"Error del servidor: {ex.Message}" });
            }
        }

        public async Task<IActionResult> HistorialCotizaciones()
        {
            // Obtener el usuarioId desde la sesión o cookies
            var usuarioJson = HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(usuarioJson))
            {
                TempData["Error"] = "Debes iniciar sesión para ver el historial de cotizaciones.";
                return RedirectToAction("Login", "Account");  // Redirigir a la página de login si no hay sesión
            }

            var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
            if (usuario == null)
            {
                TempData["Error"] = "No se pudo obtener los datos del usuario.";
                return RedirectToAction("Login", "Account");  // Redirigir si no se puede obtener el usuario
            }

            try
            {
                // Construir la URL completa para la API
                var apiUrl = $"http://localhost:5279/api/Cotizacion/usuario/{usuario.Id}"; // Base URL + endpoint
                TempData["ApiUrl"] = apiUrl;  // Guardar la URL completa en TempData para depuración

                // Hacer la solicitud a la API para obtener el historial de cotizaciones del usuario
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var cotizaciones = JsonConvert.DeserializeObject<List<Cotizacion>>(jsonResponse);

                    // Pasamos las cotizaciones a la vista
                    return View(cotizaciones);  // Mostrar la vista con las cotizaciones obtenidas
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Error al obtener el historial de cotizaciones: {errorContent}";
                    return View(new List<Cotizacion>());  // Retorna una lista vacía en caso de error
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al obtener el historial: {ex.Message}";
                return View(new List<Cotizacion>());  // Retorna una lista vacía en caso de error
            }
        }

    }



}
