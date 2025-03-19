using ProyectoAPI.DTOs;
using ProyectoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Backend.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        private UsuarioDTO ConvertirUsuarioDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                Id = usuario.Id,
                NombresCompleto = usuario.NombresCompleto,
                Correo = usuario.Correo,
                Ruc = usuario.Ruc,
                TipoUsuarioId = usuario.TipoUsuarioId ?? 0
            };
        }

        public async Task<UsuarioDTO?> AutenticarTurista(LoginDTO loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == loginDto.Correo && u.TipoUsuarioId == 2);

            // Verifica que exista el usuario y que la contraseña coincida
            if (usuario == null || usuario.Contraseña != loginDto.Contraseña)
                return null;

            return ConvertirUsuarioDTO(usuario);
        }

        public async Task<UsuarioDTO?> AutenticarDueño(LoginDueñoDTO loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Ruc == loginDto.Ruc && u.TipoUsuarioId == 3);

            // Verifica que exista el usuario y que la contraseña coincida
            if (usuario == null || usuario.Contraseña != loginDto.Contraseña)
                return null;

            return ConvertirUsuarioDTO(usuario);
        }
    }
}
