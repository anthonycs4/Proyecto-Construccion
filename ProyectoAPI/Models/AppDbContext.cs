﻿namespace ProyectoAPI.Models
{
    using Microsoft.EntityFrameworkCore;
    using ProyectoAPI.DTOs;
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
        public DbSet<ValoracionCotizacion> Tb_ValoracionCotizacion { get; set; }
        public DbSet<Estadistica> Tb_EstadisticasRecomendacion { get; set; }
        public DbSet<Cotizacion> Tb_CotizacionGuardada { get; set; }
        public DbSet<ImagenNegocio> ImagenNegocios { get; set; }
        public DbSet<ImagenServicio> ImagenServicios { get; set; }
        public DbSet<Tb_EstadisticasIA> EstadisticasIA { get; set; }
        public DbSet<ImagenServicioHotel> ImagenesServicioHotel { get; set; }
        public DbSet<ImagenServicioRestaurante> ImagenesServicioRestaurante { get; set; }
        public DbSet<ImagenServicioTuristico> ImagenesServicioTuristico { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }




    }

}
