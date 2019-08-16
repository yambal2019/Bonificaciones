using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetPedidoDetalleHistorico
    {
        public string vchCodigoCorto { get; set; }
        public string vchDescripcion { get; set; }
        public string vchCantidad { get; set; }
        public string vchPuntos { get; set; }
        public int intTotalPuntos { get; set; }
    }
}