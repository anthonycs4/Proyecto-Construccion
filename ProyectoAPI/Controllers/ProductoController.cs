using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.DTOs;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _productoService.GetAll();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _productoService.GetById(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoDTO producto)
        {
            var newProducto = await _productoService.Create(producto);
            return CreatedAtAction(nameof(Get), new { id = newProducto.Id }, newProducto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productoService.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }

}
