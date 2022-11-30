using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WikiORT.Models
{
    public class Articulo
    {
        
        public int ArticuloId { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }
        public Boolean Activo { get; set; } 
        public int CategoriaId { get; set; } //Relacion 1-1:un art tiene una categoria
        public Categoria Categoria { get; set; }
        public int AutorId { get; set; } //Relacion 1-1:un art tiene un autor
        public Autor Autor { get; set; }
        public List<Mensaje> Mensajes { get; set; }
        public List<ArticuloPalabraClave> PalabrasClaves { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }

        

    }
}
