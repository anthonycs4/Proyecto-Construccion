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
                Direccion = n.Direccion,
                Telefono = n.Telefono
            }).ToList();
        }
        public NegocioDTO? RegistrarNegocio(int usuarioId,NegocioDTO negocioDto)
        {
            var nuevoNegocio = new Negocio
            {
                UsuarioId = negocioDto.UsuarioId,
                Nombre = negocioDto.Nombre,
                TipoNegocioId = negocioDto.TipoNegocioId,
                ProvinciaId = negocioDto.ProvinciaId,
                Direccion = negocioDto.Direccion,
                Telefono = negocioDto.Telefono,
                Descripcion = negocioDto.Descripcion ?? "Sin descripción"
            };

            _context.Negocios.Add(nuevoNegocio);
            _context.SaveChanges();

            return new NegocioDTO
            {
                Id = nuevoNegocio.Id,
                UsuarioId = nuevoNegocio.UsuarioId,
                Nombre = nuevoNegocio.Nombre,
                TipoNegocioId = nuevoNegocio.TipoNegocioId,
                ProvinciaId = nuevoNegocio.ProvinciaId,
                Direccion = nuevoNegocio.Direccion,
                Telefono = nuevoNegocio.Telefono,
                Descripcion = nuevoNegocio.Descripcion
            };
        }

        public List<NegocioDTO> GetNegociosPorDueno(int usuarioId)
        {
            return _context.Negocios
                .Where(n => n.UsuarioId == usuarioId)
                .Select(n => new NegocioDTO
                {
                    Id = n.Id,
                    Nombre = n.Nombre,
                    Direccion = n.Direccion,
                    Telefono = n.Telefono
                }).ToList();
        }

        public bool ActualizarNegocio(int id, int idUsuario,NegocioDTO negocioDto)
        {
            var negocio = _context.Negocios.Find(id);
            if (negocio == null) return false;

            negocio.Nombre = negocioDto.Nombre;
            negocio.Direccion = negocioDto.Direccion;
            negocio.Telefono = negocioDto.Telefono;

            _context.SaveChanges();
            return true;
        }

        public bool EliminarNegocio(int id)
        {
            var negocio = _context.Negocios.Find(id);
            if (negocio == null) return false;

            _context.Negocios.Remove(negocio);
            _context.SaveChanges();
            return true;
        }
    }
}
