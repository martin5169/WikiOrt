using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiORT.Models
{
    public class PalabraClave
    {

        public int PalabraClaveId { get; set; }
        public string Palabra { get; set; }
        public List<ArticuloPalabraClave> Articulos { get; set; }
    }
}