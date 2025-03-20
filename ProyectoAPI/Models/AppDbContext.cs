namespace ProyectoAPI.Models
{
    using Microsoft.EntityFrameworkCore;
    using ProyectoAPI.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Negocio> Negocios { get; set; }
        public DbSet<ServicioHotel> ServiciosHotel { get; set; }
        public DbSet<ServicioRestaurante> ServiciosRestaurante { get; set; }
        public DbSet<ServicioTuristico> ServiciosTuristicos { get; set; }

    }

}
