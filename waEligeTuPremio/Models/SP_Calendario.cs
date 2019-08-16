using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_Calendario
    {
        public Int16 Año { get; set; }
        public Int16 Campaña { get; set; }
        public Int16 SemanaCampaña { get; set; }
        public Int16 SemanaAnual { get; set; }

        public string FInicio { get; set; }

        public string FFin { get; set; }
    }
}