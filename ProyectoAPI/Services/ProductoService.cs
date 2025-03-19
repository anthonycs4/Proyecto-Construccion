namespace ProyectoAPI.Services
{
    using Microsoft.EntityFrameworkCore;
    using ProyectoAPI.DTOs;
    using ProyectoAPI.Models;

    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDTO>> GetAll()
        {
            return await _context.Productos
                .Select(p => new ProductoDTO { Id = p.Id, Nombre = p.Nombre, Precio = p.Precio })
                .ToListAsync();
        }

        public async Task<ProductoDTO> GetById(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return null;
            return new ProductoDTO { Id = producto.Id, Nombre = producto.Nombre, Precio = producto.Precio };
        }

        public async Task<ProductoDTO> Create(ProductoDTO producto)
        {
            var entity = new Producto { Nombre = producto.Nombre, Precio = producto.Precio };
            _context.Productos.Add(entity);
            await _context.SaveChangesAsync();
            producto.Id = entity.Id;
            return producto;
        }

        public async Task<bool> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
