using System.Collections.Generic;

namespace WikiORT.Models
{
    public class ListaMensajesXArticulo
    {
        public int ArticuloId { get; set; }
        //public string TituloArticulo { get; set; }
        public List<Mensaje> MensajesDelArticulo { get; set; }

        public ListaMensajesXArticulo(int ArticuloId, List<Mensaje> Mensajes)
        {
           // this.ArticuloId = ArticuloId;
            this.ArticuloId = ArticuloId;
            this.MensajesDelArticulo = Mensajes;

        }
    }
}
