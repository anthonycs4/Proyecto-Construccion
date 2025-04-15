using ProyectoAPI.DTOs;
using ProyectoAPI.Models;

namespace ProyectoAPI.Services
{
    public class ServicioService
    {
        private readonly AppDbContext _context;

        public ServicioService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Obtener todos los servicios de un negocio por su tipo
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
                            CantidadPersonas=s.CantidadPersonas,
                            Precio = s.Precio,
                            WiFi=s.WiFi,
                            AguaCaliente=s.AguaCaliente,
                            RoomService=s.RoomService,
                            Cochera=s.Cochera,
                            DesayunoIncluido=s.DesayunoIncluido
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
                            Descripcion = s.Descripcion

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
                            Descripcion = s.Descripcion
                        })
                        .ToList();

                default:
                    return new List<ServicioDTO>(); // Tipo de negocio inválido
            }
        }

        // ✅ Crear un servicio según el tipo de negocio
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
                        Fotos = servicioDto.Fotos,
                        Estado = "Activo"
                    };
                    _context.ServiciosHotel.Add(nuevoServicioHotel);
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
                    break;

                default:
                    return false; // Tipo de negocio inválido
            }

            _context.SaveChanges();
            return true;
        }

        // ✅ Actualizar un servicio según el tipo de negocio
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
                    servicioHotel.Fotos = servicioDto.Fotos;
                    break;

                case "restaurante":
                    var servicioRestaurante = _context.ServiciosRestaurante.Find(id);
                    if (servicioRestaurante == null) return false;
                    servicioRestaurante.NombrePlato = servicioDto.Nombre;
                    servicioRestaurante.TipoPlato = servicioDto.TipoPlato;
                    servicioRestaurante.Precio = servicioDto.Precio;
                    servicioRestaurante.Descripcion = servicioDto.Descripcion;
                    break;

                case "turismo":
                    var servicioTuristico = _context.ServiciosTuristicos.Find(id);
                    if (servicioTuristico == null) return false;
                    servicioTuristico.NombreLugar = servicioDto.Nombre;
                    servicioTuristico.Provincia = servicioDto.Provincia;
                    servicioTuristico.Precio = servicioDto.Precio;
                    servicioTuristico.Descripcion = servicioDto.Descripcion;
                    break;

                default:
                    return false; // Tipo de negocio inválido
            }

            _context.SaveChanges();
            return true;
        }

        // ✅ Eliminar un servicio según su tipo
        public bool EliminarServicio(int id, string tipoNegocio)
        {
            switch (tipoNegocio.ToLower())
            {
                case "hotel":
                    var servicioHotel = _context.ServiciosHotel.Find(id);
                    if (servicioHotel == null) return false;
                    _context.ServiciosHotel.Remove(servicioHotel);
                    break;

                case "restaurante":
                    var servicioRestaurante = _context.ServiciosRestaurante.Find(id);
                    if (servicioRestaurante == null) return false;
                    _context.ServiciosRestaurante.Remove(servicioRestaurante);
                    break;

                case "turismo":
                    var servicioTuristico = _context.ServiciosTuristicos.Find(id);
                    if (servicioTuristico == null) return false;
                    _context.ServiciosTuristicos.Remove(servicioTuristico);
                    break;

                default:
                    return false; // Tipo de negocio inválido
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
                        Fotos = hotel.Fotos
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
                        Precio = restaurante.Precio
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
                        Precio = turismo.Precio
                    };

                default:
                    return null;
            }
        }
    }
}
