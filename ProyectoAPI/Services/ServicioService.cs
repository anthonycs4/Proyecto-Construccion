using ProyectoAPI.DTOs;
using ProyectoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoAPI.Services
{
    public class ServicioService
    {
        private readonly AppDbContext _context;

        public ServicioService(AppDbContext context)
        {
            _context = context;
        }

        public List<ServicioDTO> GetServiciosPorNegocio(int negocioId, string tipoNegocio)
        {
            switch (tipoNegocio.ToLower())
            {
                case "hotel":
                    return _context.ServiciosHotel
                        .Where(s => s.NegocioId == negocioId)
                        .Select(s => new ServicioDTO
                        {
                            Id = s.Id,
                            Nombre = "Habitación para " + s.CantidadPersonas + " personas",
                            CantidadPersonas = s.CantidadPersonas,
                            Precio = s.Precio,
                            WiFi = s.WiFi,
                            AguaCaliente = s.AguaCaliente,
                            RoomService = s.RoomService,
                            Cochera = s.Cochera,
                            DesayunoIncluido = s.DesayunoIncluido,
                            Fotos = _context.ImagenesServicioHotel.Where(f => f.ServicioHotelId == s.Id).Select(f => f.UrlImagen).ToList()
                        })
                        .ToList();

                case "restaurante":
                    return _context.ServiciosRestaurante
                        .Where(s => s.NegocioId == negocioId)
                        .Select(s => new ServicioDTO
                        {
                            Id = s.Id,
                            Nombre = s.NombrePlato,
                            TipoPlato = s.TipoPlato,
                            Precio = s.Precio,
                            Descripcion = s.Descripcion,
                            Fotos = _context.ImagenesServicioRestaurante.Where(f => f.ServicioRestauranteId == s.Id).Select(f => f.UrlImagen).ToList()
                        })
                        .ToList();

                case "turismo":
                    return _context.ServiciosTuristicos
                        .Where(s => s.NegocioId == negocioId)
                        .Select(s => new ServicioDTO
                        {
                            Id = s.Id,
                            Nombre = s.NombreLugar,
                            Precio = s.Precio,
                            Provincia = s.Provincia,
                            Descripcion = s.Descripcion,
                            Fotos = _context.ImagenesServicioTuristico.Where(f => f.ServicioTuristicoId == s.Id).Select(f => f.UrlImagen).ToList()
                        })
                        .ToList();

                default:
                    return new List<ServicioDTO>();
            }
        }

        public bool CrearServicio(int negocioId, string tipoNegocio, ServicioDTO servicioDto)
        {
            switch (tipoNegocio.ToLower())
            {
                case "hotel":
                    var nuevoServicioHotel = new ServicioHotel
                    {
                        NegocioId = negocioId,
                        CantidadPersonas = servicioDto.CantidadPersonas,
                        WiFi = servicioDto.WiFi,
                        AguaCaliente = servicioDto.AguaCaliente,
                        RoomService = servicioDto.RoomService,
                        Cochera = servicioDto.Cochera,
                        Cable = servicioDto.Cable,
                        DesayunoIncluido = servicioDto.DesayunoIncluido,
                        Precio = servicioDto.Precio,
                        Estado = "Activo"
                    };
                    _context.ServiciosHotel.Add(nuevoServicioHotel);
                    _context.SaveChanges();

                    // Guardar las fotos
                    if (servicioDto.Fotos != null)
                    {
                        foreach (var fotoUrl in servicioDto.Fotos)
                        {
                            _context.ImagenesServicioHotel.Add(new ImagenServicioHotel
                            {
                                ServicioHotelId = nuevoServicioHotel.Id,
                                UrlImagen = fotoUrl
                            });
                        }
                        _context.SaveChanges();
                    }
                    break;

                case "restaurante":
                    var nuevoServicioRestaurante = new ServicioRestaurante
                    {
                        NegocioId = negocioId,
                        NombrePlato = servicioDto.Nombre,
                        TipoPlato = servicioDto.TipoPlato,
                        Precio = servicioDto.Precio,
                        Descripcion = servicioDto.Descripcion
                    };
                    _context.ServiciosRestaurante.Add(nuevoServicioRestaurante);
                    _context.SaveChanges();

                    if (servicioDto.Fotos != null)
                    {
                        foreach (var fotoUrl in servicioDto.Fotos)
                        {
                            _context.ImagenesServicioRestaurante.Add(new ImagenServicioRestaurante
                            {
                                ServicioRestauranteId = nuevoServicioRestaurante.Id,
                                UrlImagen = fotoUrl
                            });
                        }
                        _context.SaveChanges();
                    }
                    break;

                case "turismo":
                    var nuevoServicioTuristico = new ServicioTuristico
                    {
                        NegocioId = negocioId,
                        NombreLugar = servicioDto.Nombre,
                        Provincia = servicioDto.Provincia,
                        Precio = servicioDto.Precio,
                        Descripcion = servicioDto.Descripcion
                    };
                    _context.ServiciosTuristicos.Add(nuevoServicioTuristico);
                    _context.SaveChanges();

                    if (servicioDto.Fotos != null)
                    {
                        foreach (var fotoUrl in servicioDto.Fotos)
                        {
                            _context.ImagenesServicioTuristico.Add(new ImagenServicioTuristico
                            {
                                ServicioTuristicoId = nuevoServicioTuristico.Id,
                                UrlImagen = fotoUrl
                            });
                        }
                        _context.SaveChanges();
                    }
                    break;

                default:
                    return false;
            }

            return true;
        }

        public bool ActualizarServicio(int id, string tipoNegocio, ServicioDTO servicioDto)
        {
            switch (tipoNegocio.ToLower())
            {
                case "hotel":
                    var servicioHotel = _context.ServiciosHotel.Find(id);
                    if (servicioHotel == null) return false;

                    servicioHotel.CantidadPersonas = servicioDto.CantidadPersonas;
                    servicioHotel.WiFi = servicioDto.WiFi;
                    servicioHotel.AguaCaliente = servicioDto.AguaCaliente;
                    servicioHotel.RoomService = servicioDto.RoomService;
                    servicioHotel.Cochera = servicioDto.Cochera;
                    servicioHotel.Cable = servicioDto.Cable;
                    servicioHotel.DesayunoIncluido = servicioDto.DesayunoIncluido;
                    servicioHotel.Precio = servicioDto.Precio;
                    _context.SaveChanges();

                    // Actualizar fotos (eliminar viejas y guardar nuevas)
                    var fotosHotel = _context.ImagenesServicioHotel.Where(f => f.ServicioHotelId == id).ToList();
                    _context.ImagenesServicioHotel.RemoveRange(fotosHotel);
                    _context.SaveChanges();

                    if (servicioDto.Fotos != null)
                    {
                        foreach (var fotoUrl in servicioDto.Fotos)
                        {
                            _context.ImagenesServicioHotel.Add(new ImagenServicioHotel
                            {
                                ServicioHotelId = id,
                                UrlImagen = fotoUrl
                            });
                        }
                        _context.SaveChanges();
                    }
                    break;

                case "restaurante":
                    var servicioRestaurante = _context.ServiciosRestaurante.Find(id);
                    if (servicioRestaurante == null) return false;

                    servicioRestaurante.NombrePlato = servicioDto.Nombre;
                    servicioRestaurante.TipoPlato = servicioDto.TipoPlato;
                    servicioRestaurante.Precio = servicioDto.Precio;
                    servicioRestaurante.Descripcion = servicioDto.Descripcion;
                    _context.SaveChanges();

                    var fotosRestaurante = _context.ImagenesServicioRestaurante.Where(f => f.ServicioRestauranteId == id).ToList();
                    _context.ImagenesServicioRestaurante.RemoveRange(fotosRestaurante);
                    _context.SaveChanges();

                    if (servicioDto.Fotos != null)
                    {
                        foreach (var fotoUrl in servicioDto.Fotos)
                        {
                            _context.ImagenesServicioRestaurante.Add(new ImagenServicioRestaurante
                            {
                                ServicioRestauranteId = id,
                                UrlImagen = fotoUrl
                            });
                        }
                        _context.SaveChanges();
                    }
                    break;

                case "turismo":
                    var servicioTuristico = _context.ServiciosTuristicos.Find(id);
                    if (servicioTuristico == null) return false;

                    servicioTuristico.NombreLugar = servicioDto.Nombre;
                    servicioTuristico.Provincia = servicioDto.Provincia;
                    servicioTuristico.Precio = servicioDto.Precio;
                    servicioTuristico.Descripcion = servicioDto.Descripcion;
                    _context.SaveChanges();

                    var fotosTuristico = _context.ImagenesServicioTuristico.Where(f => f.ServicioTuristicoId == id).ToList();
                    _context.ImagenesServicioTuristico.RemoveRange(fotosTuristico);
                    _context.SaveChanges();

                    if (servicioDto.Fotos != null)
                    {
                        foreach (var fotoUrl in servicioDto.Fotos)
                        {
                            _context.ImagenesServicioTuristico.Add(new ImagenServicioTuristico
                            {
                                ServicioTuristicoId = id,
                                UrlImagen = fotoUrl
                            });
                        }
                        _context.SaveChanges();
                    }
                    break;

                default:
                    return false;
            }

            return true;
        }

        public bool EliminarServicio(int id, string tipoNegocio)
        {
            switch (tipoNegocio.ToLower())
            {
                case "hotel":
                    var servicioHotel = _context.ServiciosHotel.Find(id);
                    if (servicioHotel == null) return false;

                    var fotosHotel = _context.ImagenesServicioHotel.Where(f => f.ServicioHotelId == id).ToList();
                    _context.ImagenesServicioHotel.RemoveRange(fotosHotel);
                    _context.ServiciosHotel.Remove(servicioHotel);
                    break;

                case "restaurante":
                    var servicioRestaurante = _context.ServiciosRestaurante.Find(id);
                    if (servicioRestaurante == null) return false;

                    var fotosRestaurante = _context.ImagenesServicioRestaurante.Where(f => f.ServicioRestauranteId == id).ToList();
                    _context.ImagenesServicioRestaurante.RemoveRange(fotosRestaurante);
                    _context.ServiciosRestaurante.Remove(servicioRestaurante);
                    break;

                case "turismo":
                    var servicioTuristico = _context.ServiciosTuristicos.Find(id);
                    if (servicioTuristico == null) return false;

                    var fotosTuristico = _context.ImagenesServicioTuristico.Where(f => f.ServicioTuristicoId == id).ToList();
                    _context.ImagenesServicioTuristico.RemoveRange(fotosTuristico);
                    _context.ServiciosTuristicos.Remove(servicioTuristico);
                    break;

                default:
                    return false;
            }

            _context.SaveChanges();
            return true;
        }

        public ServicioDTO? GetServicioPorId(int id, string tipoServicio)
        {
            switch (tipoServicio.ToLower())
            {
                case "hotel":
                    var hotel = _context.ServiciosHotel.FirstOrDefault(s => s.Id == id);
                    if (hotel == null) return null;

                    return new ServicioDTO
                    {
                        Id = hotel.Id,
                        CantidadPersonas = hotel.CantidadPersonas,
                        WiFi = hotel.WiFi,
                        AguaCaliente = hotel.AguaCaliente,
                        RoomService = hotel.RoomService,
                        Cochera = hotel.Cochera,
                        Cable = hotel.Cable,
                        DesayunoIncluido = hotel.DesayunoIncluido,
                        Precio = hotel.Precio,
                        Fotos = _context.ImagenesServicioHotel.Where(f => f.ServicioHotelId == id).Select(f => f.UrlImagen).ToList()
                    };

                case "restaurante":
                    var restaurante = _context.ServiciosRestaurante.FirstOrDefault(s => s.Id == id);
                    if (restaurante == null) return null;

                    return new ServicioDTO
                    {
                        Id = restaurante.Id,
                        Nombre = restaurante.NombrePlato,
                        TipoPlato = restaurante.TipoPlato,
                        Descripcion = restaurante.Descripcion,
                        Precio = restaurante.Precio,
                        Fotos = _context.ImagenesServicioRestaurante.Where(f => f.ServicioRestauranteId == id).Select(f => f.UrlImagen).ToList()
                    };

                case "turismo":
                    var turismo = _context.ServiciosTuristicos.FirstOrDefault(s => s.Id == id);
                    if (turismo == null) return null;

                    return new ServicioDTO
                    {
                        Id = turismo.Id,
                        Provincia = turismo.Provincia,
                        Nombre = turismo.NombreLugar,
                        Descripcion = turismo.Descripcion,
                        Precio = turismo.Precio,
                        Fotos = _context.ImagenesServicioTuristico.Where(f => f.ServicioTuristicoId == id).Select(f => f.UrlImagen).ToList()
                    };

                default:
                    return null;
            }
        }
    }
}
