using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetPedidoMail
    {
        public string VchNombreCompleto { get; set; }
        public string VchEmail { get; set; }
        public string VchDireccionL1 { get; set; }
        public string VchDistrito { get; set; }
        public string VchCiudad { get; set; }
        public string VchDepartamento { get; set; }
        public Int32 IntPedido { get; set; }
        public DateTime DttmFecha { get; set; }
        public string VchPremio { get; set; }
        public string VchGanamas {get; set;}
        //public byte[] imgImagen { get; set; }
        public string VchMensaje { get; set; }
        public string CodConsultora { get; set; }
        public string VchGanamasNivelSAPTitulo { get; set; }
    }
}