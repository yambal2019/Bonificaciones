using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetPedidoTemp
    {
        [Key]
        [Required]
        public int intPedido { get; set; }
        [Required]
        public int intCodigoConsultora { get; set; }
        [Required]
        public Int16 smintNano { get; set; }
        [Required]
        public Int16 smintCampana { get; set; }
        [Required]
        public Int16 smintSemana { get; set; }
        [Required]
        public string vchPlan { get; set; }
        [Required]
        public Int16 smintPuntosPlan { get; set; }
        [Required]
        public DateTime dttmFecha { get; set; }


        public ICollection<SP_GetPedidoDetalleTemp> Detalle { get; set; }
        public SP_GetPedidoTemp()
        {
            Detalle = new List<SP_GetPedidoDetalleTemp>();
        }
    }
}