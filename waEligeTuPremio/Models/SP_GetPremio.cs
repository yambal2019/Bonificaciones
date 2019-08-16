using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace waEligeTuPremio.Models
{
    public class SP_GetPremio
    {
        [Key]
        [Required]
        public string vchPlan { get; set; }
        [Required]
        public Int16 smintAnioIniPremio { get; set; }
        [Required]
        public Int16 smintCampIniPremio { get; set; }
        [Required]
        public Int16 smintAnioFinPremio { get; set; }
        [Required]
        public Int16 smintCampFinPremio { get; set; }
        [Required]
        public Int16 smintAnioIniPedido { get; set; }
        [Required]
        public Int16 smintCampIniPedido { get; set; }
        [Required]
        public Int16 smintAnioFinPedido { get; set; }
        [Required]
        public Int16 smintCampFinPedido { get; set; }
        [Key]
        [Required]
        [Remote("CodigoSAPValido", "Premios", ErrorMessage = "El Código SAP duplicado.")]
        [Display(Name = "Cod SAP")]
        public int intCodigoSAP { get; set; }
        [Required]
        [Remote("CodigoCortoValido", "Premios", ErrorMessage = "El Código Corto duplicado.")]
        [Display(Name = "Cod Corto")]
        public int intCodigoCorto { get; set; }

        [Required]
        [Display(Name = "Orden")]
        public int intOrden { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        public string vchTitulo { get; set; }

        [Required]
        [Display(Name = "Producto")]
        public string vchDescripcion { get; set; }
        [Required]
        [Display(Name = "Stock")]
        public Int16 smintStock { get; set; }
        [Required]
        public Int16 smintStockrReal { get; set; }
        [Required]
        [Display(Name = "Puntos")]
        public Int16 smintPuntos { get; set; }
        public byte[] imgImagen { get; set; }
        [Display(Name = "Activo")]
        public bool bitActivo { get; set; }
        [Required]
        public int intUsr { get; set; }
        [Display(Name = "Nivel")]
        public int intNivel { get; set; }
        [Display(Name = "Producto Inicial")]
        public bool bitInicial { get; set; }
        public string VchGanamasNivelSAPTitulo { get; set; }

        public Int32 intCodigo { get; set; }
        public List<Nivel> ListaNivel   { get; set; }
}

    public class SP_GetListaPlan
    {
        public int intCodigo { get; set; }
        public string vchPlan { get; set; }
    }

public class Nivel
{

    public int intNivel { get; set; }
    public int cantidadNivel { get; set; }
    }

}