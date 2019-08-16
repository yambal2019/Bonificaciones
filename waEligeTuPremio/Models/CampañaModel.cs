using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace waEligeTuPremio.Models
{
    public class CampañaModel
    {
        public Int32 intCampania { get; set; }
        public Int32 vchDescripcion { get; set; }
        public Int32 smintAnioIniCampania { get; set; }
        public Int32 smintCampIniCampania { get; set; }
        public Int32 smintAnioFinCampania { get; set; }
        public Int32 smintCampFinCampania { get; set; }
        public Boolean bitActivo { get; set; }
        public DateTime dttmFecha { get; set; }

        public SelectList ListaCampaña { get; set; }

    }
}