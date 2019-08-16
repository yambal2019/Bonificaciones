using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetFormatoPremio
    {
        public string Plan { get; set; }
        public Int16 AnioIniPremio { get; set; }
        public Int16 CampIniPremio { get; set; }
        public Int16 AnioFinPremio { get; set; }
        public Int16 CampFinPremio { get; set; }

        public Int16 AnioIniPedido { get; set; }
        public Int16 CampIniPedido { get; set; }
        public Int16 AnioFinPedido { get; set; }
        public Int16 CampFinPedido { get; set; }

        public int CodigoSAP { get; set; }
        public int CodigoCorto { get; set; }
        public int Orden { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Int16 Stock { get; set; }

    }
}