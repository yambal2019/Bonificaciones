using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_ObtenerEmailServidor
    {
        public string VchFrom { get; set; }
        public string VchHost { get; set; }
        public string VchUserName { get; set; }
        public string VchPassword { get; set; }
        public int IntPort { get; set; }
        public string VchCopiaMail { get; set; }

    }
}