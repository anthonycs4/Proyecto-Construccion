using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoAPI.Models
{
    [Table("Tb_Negocio")] // Especifica el nombre real de la tabla en la BD

    public class Negocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int UsuarioId { get; set; }  // Relación con el dueño del negocio
        public Usuario Usuario { get; set; }
        public List<Hospedaje> Hospedajes { get; set; }
    }

}
