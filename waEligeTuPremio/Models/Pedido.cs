using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class Pedido
    {
        public Int32 intPedido { get; set; }
        public Int32 intCodigoConsultora { get; set; }
        public Int32 smintNano { get; set; }
        public Int32 smintCampana { get; set; }
        public Int32 smintSemana { get; set; }
        public String vchPlan { get; set; }
        public Int32 smintPuntosPlan { get; set; }
        public DateTime dttmFecha { get; set; }
        public String vchEstadoDescarga { get; set; }

    }
}