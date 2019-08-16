using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_Perfil
    {
        [Key]
        public int intCodigo { get; set; }
        [Required(ErrorMessage = "* El Nombre esta vacío.")]
        [Display(Name = "Nombre")]
        public string vchNombre { get; set; }
    }
}