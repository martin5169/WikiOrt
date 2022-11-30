using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WikiORT.Models
{
    public class ArticuloPalabraClave
    {
        public int ArticuloPalabraClaveId { get; set; }
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
        public int PalabraClaveId { get; set; }
        public PalabraClave PalabraClave { get; set; }

    }
}
