using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WikiORT.Models
{
    public class Mensaje
    {
        public int MensajeId { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }


    }
}
