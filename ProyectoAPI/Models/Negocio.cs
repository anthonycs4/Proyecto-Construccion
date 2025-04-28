using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_Negocio")] // Especifica el nombre real de la tabla en la BD

    public class Negocio
    {
        [Key]

        public int Id { get; set; }
        public int UsuarioId { get; set; }

        [Required, MaxLength(70)]
        public string Nombre { get; set; }

        public int TipoNegocioId { get; set; }
        public int ProvinciaId { get; set; }

        [Required, MaxLength(100)]
        public string Direccion { get; set; }

        public string Descripcion { get; set; }

        [Required, MaxLength(9)]
        public string Telefono { get; set; }

        public List<ServicioHotel> ServiciosHotel { get; set; }
        public List<ServicioRestaurante> ServiciosRestaurante { get; set; }
        public List<ServicioTuristico> ServiciosTuristico { get; set; }
        public List<ImagenNegocio> Imagenes { get; set; }


    }

}
