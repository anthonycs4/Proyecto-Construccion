using ProyectoAPI.DTOs;
using ProyectoAPI.Models;

namespace ProyectoAPI.Services
{
    public class NegocioService
    {
        private readonly AppDbContext _context;

        public NegocioService(AppDbContext context)
        {
            _context = context;
        }

        public List<NegocioDTO> GetNegocios()
        {
            return _context.Negocios.Select(n => new NegocioDTO
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Descripcion = n.Descripcion,
                Direccion = n.Direccion,
                Telefono = n.Telefono
            }).ToList();
        }
        public List<NegocioDTO> GetNegociosPorDueno(int usuarioId)
        {
            return _context.Negocios
                .Where(n => n.UsuarioId == usuarioId)
                .Select(n => new NegocioDTO
                {
                    Id = n.Id,
                    Nombre = n.Nombre,
                    Descripcion = n.Descripcion,
                    Direccion = n.Direccion,
                    Telefono = n.Telefono
                }).ToList();
        }
        public bool ActualizarNegocio(int id, NegocioDTO negocioDto)
        {
            var negocio = _context.Negocios.Find(id);
            if (negocio == null) return false;

            negocio.Nombre = negocioDto.Nombre;
            negocio.Descripcion = negocioDto.Descripcion;
            negocio.Direccion = negocioDto.Direccion;
            negocio.Telefono = negocioDto.Telefono;

            _context.SaveChanges();
            return true;
        }

    }
}
