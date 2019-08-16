using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetReemplazoPremio
    {
        public int Codigo_CNS { get; set; }
        public int Incentivo { get; set; }
        public Int16 Nivel { get; set; }
        public string NombPlan { get; set; }
        public string Periodo { get; set; }
        public string Regla { get; set; }
        public int SustituteKey { get; set; }
        public string AwardPeriodo { get; set; }
        public string CodigoPremios { get; set; }
        public DateTime FechaEleccion { get; set; }
    }
}