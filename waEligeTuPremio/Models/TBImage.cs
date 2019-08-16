using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class TBImage
    {
        
        public int intImagen { get; set; }

        
        public string vchNombre { get; set; }

       
        public string vchExtencion { get; set; }

        public Byte[] vchImagen { get; set; }
        public int intPremio { get; set; }
    }
}