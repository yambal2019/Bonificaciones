using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetPedidoHistorico
    {
        [Key]
        [Required]
        public int intPedido { get; set; }
        [Required]
        public int intCodigoConsultora { get; set; }
        [Required]
        public string vchNombreCompleto { get; set; }
        [Required]
        public string vchPlan { get; set; }
        [Required]
        public Int16 smintPuntosPlan { get; set; }
        [Required]
        public string dttmFecha { get; set; }
    }
}