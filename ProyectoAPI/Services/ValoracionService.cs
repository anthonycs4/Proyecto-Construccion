using ProyectoAPI.DTOs;
using ProyectoAPI.Models;
using System.Collections.Generic;
using System.Linq;

public class ValoracionService
{
    private readonly AppDbContext _context;

    public ValoracionService(AppDbContext context)
    {
        _context = context;
    }

    public void GuardarValoracion(ValoracionCotizacionDTO dto)
    {
        var valoracion = new ValoracionCotizacion
        {
            Estrellas = dto.Estrellas,
            Comentario = dto.Comentario
        };

        _context.Tb_ValoracionCotizacion.Add(valoracion);
        _context.SaveChanges();
    }

    public List<ValoracionCotizacion> ObtenerValoraciones()
    {
        return _context.Tb_ValoracionCotizacion
                       .OrderByDescending(v => v.FechaRegistro)
                       .ToList();
    }

    public double ObtenerPromedioEstrellas()
    {
        return _context.Tb_ValoracionCotizacion.Any()
            ? _context.Tb_ValoracionCotizacion.Average(v => v.Estrellas)
            : 0;
    }
}
