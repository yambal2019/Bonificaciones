using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class TBPedidoModel
    {

        public Int32 intPedido { get; set; }
        public Int32? intCodigoConsultora { get; set; }
        public Int32? smintNano { get; set; }
        public Int32? smintCampana { get; set; }
        public Int32? smintSemana { get; set; }
        public Int32? smintPuntosPedido { get; set; }
        public DateTime? dttmFecha { get; set; }
        public String vchEstadoDescarga { get; set; }
        public Int32? intCampaña { get; set; }

    }
}