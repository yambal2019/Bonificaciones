using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetPedidoDetalleTemp
    {
        [Key]
        [Required]
        public int intPedido { get; set; }
        [Key]
        [Required]
        public int intCodigoSAP { get; set; }
        [Required]
        public int intCodigoCorto { get; set; }
        [Required]
        public string vchDescripcion { get; set; }
        [Required]
        public Int16 smintCantidad { get; set; }
        [Required]
        public Int16 smintPuntos { get; set; }
        [Required]
        public int intTotalPuntos { get; set; }
        [Required]
        public byte[] imgImagen { get; set; }

        public Int16 smintStockReal { get; set; }

        public string VchGanamasNivelSAPTitulo { get; set; }
    }
}