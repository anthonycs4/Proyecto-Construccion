﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Construccion.Models
{
    [Table("Tb_Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [StringLength(8)]
        public string? DniRut { get; set; }

        [StringLength(11)]
        public string? Ruc { get; set; }

        [Required]
        [StringLength(50)]
        public string NombresCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        public int? TipoUsuarioId { get; set; }

        [Required]
        public int Estado { get; set; }

        [StringLength(50)]
        public string? Contraseña { get; set; }



        //revisar
     
        public class LoginRequestDTO
        {
            public string? Correo { get; set; }
            public string? Contraseña { get; set; }
        }

        public class LoginDueñoRequestDTO
        {
            public string? Ruc { get; set; }
            public string? Contraseña { get; set; }
        }
    }

}

