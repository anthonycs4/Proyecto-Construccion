using Microsoft.EntityFrameworkCore;
using ProyectoAPI.DTOs;
using ProyectoAPI.Models;

namespace ProyectoAPI.Services
{
    public class FeedbackService
    {
        private readonly AppDbContext _context;

        public IEnumerable<object> Negocios { get; internal set; }

        public FeedbackService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Feedback>> GetFeedbacksPorNegocio(int negocioId)
        {
            return await _context.Feedbacks
                                 .Where(f => f.NegocioId == negocioId)
                                 .ToListAsync();
        }
        public async Task<FeedbackDTO> CreateAsync(FeedbackDTO dto)
        {
            var feedback = new Feedback
            {
                NegocioId = dto.NegocioId,
                UsuarioId = dto.UsuarioId,
                Comentario = dto.Comentario,
                Calificacion = dto.Calificacion,
                Fecha = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            dto.Id = feedback.Id;
            dto.Fecha = feedback.Fecha;
            return dto;
        }

        public async Task<List<FeedbackDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Feedbacks
                .Where(f => f.UsuarioId == usuarioId)
                .Select(f => new FeedbackDTO
                {
                    Id = f.Id,
                    NegocioId = f.NegocioId,
                    UsuarioId = f.UsuarioId,
                    Comentario = f.Comentario,
                    Calificacion = f.Calificacion,
                    Fecha = f.Fecha
                })
            .ToListAsync();
        }

        public async Task<List<FeedbackDTO>> GetByNegocioIdAsync(int negocioId)
        {
            return await _context.Feedbacks
                .Where(f => f.NegocioId == negocioId)
                .Select(f => new FeedbackDTO
                {
                    Id = f.Id,
                    NegocioId = f.NegocioId,
                    UsuarioId = f.UsuarioId,
                    Comentario = f.Comentario,
                    Calificacion = f.Calificacion,
                    Fecha = f.Fecha
                })
                .ToListAsync();
        }
    }

}
