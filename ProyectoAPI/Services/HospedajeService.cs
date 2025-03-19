using Microsoft.EntityFrameworkCore;
using ProyectoAPI.DTOs;
using ProyectoAPI.Models;

namespace ProyectoAPI.Services
{
    public class HospedajeService
    {
        private readonly AppDbContext _context;

        public HospedajeService(AppDbContext context)
        {
            _context = context;
        }

        public bool ActualizarHospedaje(int id, HospedajeDTO hospedajeDto)
        {
            var hospedaje = _context.Hospedajes.Find(id);
            if (hospedaje == null) return false;

            hospedaje.Tipo = hospedajeDto.Tipo;
            hospedaje.Capacidad = hospedajeDto.Capacidad;
            hospedaje.PrecioPorNoche = hospedajeDto.PrecioPorNoche;

            _context.SaveChanges();
            return true;
        }

    }
}
