using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Construccion.Models
{
    public class Negocio
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int TipoNegocioId { get; set; }
        public int ProvinciaId { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string? Descripcion { get; set; }

    }
}
