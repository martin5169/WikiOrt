using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiORT.Models
{
    public class Administrador : Usuario
    {
        //public int UsuarioId { get; set; }
        public int AdministradorId { get; set; }
    }
}