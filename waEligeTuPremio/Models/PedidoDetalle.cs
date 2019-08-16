using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class PedidoDetalle
    {

        public Int32 intPedido { get; set; }
        public Int32 intCodigoSAP { get; set; }
        public Int32 intCodigoCorto { get; set; }
        public String vchDescripcion { get; set; }
        public Int32 smintCantidad { get; set; }
        public Int32 smintPuntos { get; set; }
        public Int32 intTotalPuntos { get; set; }
        public String imgImagen  { get; set; }
        public Int32 intSustituteKey { get; set; }

    }
}