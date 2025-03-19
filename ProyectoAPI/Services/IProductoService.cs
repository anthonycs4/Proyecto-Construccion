using ProyectoAPI.DTOs;

namespace ProyectoAPI.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> GetAll();
        Task<ProductoDTO> GetById(int id);
        Task<ProductoDTO> Create(ProductoDTO producto);
        Task<bool> Delete(int id);
    }

}
