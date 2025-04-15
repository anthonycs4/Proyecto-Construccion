using ProyectoAPI.Models;

namespace ProyectoAPI.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegistrarUsuarioCliente(Usuario dto)
        {
            var usuario = new Usuario
            {
                DniRut = dto.DniRut,
                Ruc = null,
                NombresCompleto = dto.NombresCompleto,
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                TipoUsuarioId = 2,
                Estado = 1
            };

            _context.Usuarios.Add(usuario);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> RegistrarUsuarioNegocio(Usuario dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Ruc))
                return false;

            var usuario = new Usuario
            {
                DniRut = dto.DniRut,
                Ruc = dto.Ruc,
                NombresCompleto = dto.NombresCompleto,
                Correo = dto.Correo,
                Contraseña = dto.Contraseña,
                TipoUsuarioId = 3, // Dueño de negocio
                Estado = 1
            };

            _context.Usuarios.Add(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

    }

}
