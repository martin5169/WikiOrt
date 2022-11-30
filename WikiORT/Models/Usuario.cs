using System;
using System.ComponentModel.DataAnnotations;

namespace WikiORT.Models
{
    public class Usuario
    {
       // public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [Display(Name = "Fecha alta")]
        public DateTime FechaAlta { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public String getNombre() {
            return Nombre;
        }
    }
}