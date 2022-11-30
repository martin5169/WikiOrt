using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WikiORT.Models
{
    public class Autor : Usuario
    {
        public int AutorId { get; set; }
        public List<Articulo> Articulos { get; set; }

        }
}
