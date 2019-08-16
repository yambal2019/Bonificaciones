using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetMensaje
    {
        [Display(Name = "Titpo")]
        public string vchTipo { get; set; }

        [Key]
        [Required]
        public int intCodigo { get; set; }
        [Required(ErrorMessage = "* El Mensaje esta vacío.")]
        [Display(Name = "Mensaje")]
        public string vchMensaje { get; set; }

        //[Required]
        [Display(Name = "Activo")]
        //[Remote("MensajeActivo", "Mensaje", ErrorMessage = "* Solo se permite un mensaje Activo.")]
        public bool BitActivo { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dttmFecha { get; set; }

        public int Cantidad { get; set; }

        public string JSFuncion { get; set; }
        public string JSMensaje { get; set; }
    }
}