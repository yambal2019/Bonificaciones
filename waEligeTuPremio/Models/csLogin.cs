using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class csLogin
    {
        [Required(ErrorMessage = "* El Código esta vacío.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "* La Contraseña esta vacía.")]
        public string Contrasena { get; set; }

        public string JsFunction { get; set; }
    }
}