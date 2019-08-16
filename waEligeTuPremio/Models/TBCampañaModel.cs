using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace waEligeTuPremio.Models
{
    public class TBCampañaModel
    {
        public Int32? intCampaña { get; set; }
        public string vchDescripcion { get; set; }
        public Int16? smintAnio { get; set; }
        public Int16? smintCampania { get; set; }
     
        public DateTime? dttmFechaInicio { get; set; }
        public DateTime? dttmFechaFin { get; set; }
        public bool? bitEstado { get; set; }
        public SelectList ListaCampaña { get; set; }
        public int SelectedCampañaId { get; set; }

    }
}