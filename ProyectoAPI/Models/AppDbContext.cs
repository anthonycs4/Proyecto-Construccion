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
        public DbSet<Hospedaje> Hospedajes { get; set; }  // <- Asegúrate de que exista


    }

}
