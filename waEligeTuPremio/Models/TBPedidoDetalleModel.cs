using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class TBPedidoDetalleModel
    {
        public Int32 intPedidoDetalle { get; set; }
        public Int32? intTotalPuntos { get; set; }
        public Int32? intPremio { get; set; }
        public Int32? intPedido { get; set; }
        public Boolean bitSeleccionado { get; set; }


    }
}