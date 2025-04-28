using Microsoft.EntityFrameworkCore;
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

        // ✅ Obtener todos los negocios (con imágenes)
        public List<NegocioDTO> GetNegocios()
        {
            return _context.Negocios
                .Include(n => n.Imagenes) // Incluir las imágenes correctamente
                .Select(n => new NegocioDTO
                {
                    Id = n.Id,
                    Nombre = n.Nombre,
                    Direccion = n.Direccion,
                    Telefono = n.Telefono,
                    Imagenes = n.Imagenes.Select(img => img.UrlImagen).ToList() // Accediendo a la propiedad UrlImagen
                })
                .ToList();
        }


        // ✅ Obtener detalles de un negocio (con imágenes)
        public async Task<NegocioDTO> ObtenerDetallesNegocioAsync(int id)
        {
            var negocio = await _context.Negocios
                .Include(n => n.Imagenes)
                .Where(n => n.Id == id)
                .Select(n => new NegocioDTO
                {
                    Id = n.Id,
                    UsuarioId = n.UsuarioId,
                    Nombre = n.Nombre,
                    TipoNegocioId = n.TipoNegocioId,
                    ProvinciaId = n.ProvinciaId,
                    Direccion = n.Direccion,
                    Telefono = n.Telefono,
                    Descripcion = n.Descripcion,
                    Imagenes = n.Imagenes.Select(img => img.UrlImagen).ToList()
                })
                .FirstOrDefaultAsync();

            if (negocio == null)
            {
                throw new Exception("Negocio no encontrado");
            }

            return negocio;
        }

        // ✅ Registrar un nuevo negocio (y guardar imágenes)
        public NegocioDTO? RegistrarNegocio(int usuarioId, NegocioDTO negocioDto)
        {
            var nuevoNegocio = new Negocio
            {
                UsuarioId = usuarioId,
                Nombre = negocioDto.Nombre,
                TipoNegocioId = negocioDto.TipoNegocioId,
                ProvinciaId = negocioDto.ProvinciaId,
                Direccion = negocioDto.Direccion,
                Telefono = negocioDto.Telefono,
                Descripcion = negocioDto.Descripcion ?? "Sin descripción"
            };

            _context.Negocios.Add(nuevoNegocio);
            _context.SaveChanges();

            // 📸 Guardar imágenes si vienen
            if (negocioDto.Imagenes != null && negocioDto.Imagenes.Count > 0)
            {
                foreach (var url in negocioDto.Imagenes)
                {
                    _context.ImagenNegocios.Add(new ImagenNegocio
                    {
                        NegocioId = nuevoNegocio.Id,
                        UrlImagen = url
                    });
                }
                _context.SaveChanges();
            }

            return new NegocioDTO
            {
                Id = nuevoNegocio.Id,
                UsuarioId = nuevoNegocio.UsuarioId,
                Nombre = nuevoNegocio.Nombre,
                TipoNegocioId = nuevoNegocio.TipoNegocioId,
                ProvinciaId = nuevoNegocio.ProvinciaId,
                Direccion = nuevoNegocio.Direccion,
                Telefono = nuevoNegocio.Telefono,
                Descripcion = nuevoNegocio.Descripcion,
                Imagenes = negocioDto.Imagenes
            };
        }

        // ✅ Obtener negocios por dueño (con imágenes)
        public List<NegocioDTO> GetNegociosPorDueno(int usuarioId)
        {
            return _context.Negocios
                .Where(n => n.UsuarioId == usuarioId)
                .Include(n => n.Imagenes)
                .Select(n => new NegocioDTO
                {
                    Id = n.Id,
                    Nombre = n.Nombre,
                    Direccion = n.Direccion,
                    Telefono = n.Telefono,
                    TipoNegocioId = n.TipoNegocioId,
                    Descripcion=n.Descripcion,
                    Imagenes = n.Imagenes.Select(img => img.UrlImagen).ToList()
                })
                .ToList();
        }

        // ✅ Actualizar negocio (actualizar imágenes también)
        public bool ActualizarNegocio(int id, int idUsuario, NegocioDTO negocioDto)
        {
            var negocio = _context.Negocios
                .Include(n => n.Imagenes)
                .FirstOrDefault(n => n.Id == id && n.UsuarioId == idUsuario);

            if (negocio == null) return false;

            negocio.Nombre = negocioDto.Nombre;
            negocio.Direccion = negocioDto.Direccion;
            negocio.Telefono = negocioDto.Telefono;

            // 🧹 Borrar imágenes antiguas
            var imagenesAntiguas = _context.ImagenNegocios.Where(img => img.NegocioId == id).ToList();
            _context.ImagenNegocios.RemoveRange(imagenesAntiguas);

            // 📸 Agregar nuevas imágenes
            if (negocioDto.Imagenes != null && negocioDto.Imagenes.Count > 0)
            {
                foreach (var url in negocioDto.Imagenes)
                {
                    _context.ImagenNegocios.Add(new ImagenNegocio
                    {
                        NegocioId = id,
                        UrlImagen = url
                    });
                }
            }

            _context.SaveChanges();
            return true;
        }

        // ✅ Eliminar negocio (eliminar imágenes también)
        public bool EliminarNegocio(int id)
        {
            var negocio = _context.Negocios
                .Include(n => n.Imagenes)
                .FirstOrDefault(n => n.Id == id);

            if (negocio == null) return false;

            // 🧹 Borrar estadísticas asociadas
            var estadisticas = _context.EstadisticasIA.Where(e => e.NegocioId == id).ToList();
            if (estadisticas.Count > 0)
            {
                _context.EstadisticasIA.RemoveRange(estadisticas);
            }

            // 🧹 Borrar imágenes asociadas
            var imagenes = _context.ImagenNegocios.Where(img => img.NegocioId == id).ToList();
            if (imagenes.Count > 0)
            {
                _context.ImagenNegocios.RemoveRange(imagenes);
            }

            // 🗑️ Borrar negocio
            _context.Negocios.Remove(negocio);

            _context.SaveChanges();
            return true;
        }

    }
}
