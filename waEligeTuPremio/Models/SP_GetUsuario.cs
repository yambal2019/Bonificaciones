using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_GetUsuario
    {
        [Key]
        public int intCodigo { get; set; }
        [Required(ErrorMessage = "* El Usuario esta vacío.")]
        [Display(Name = "Usuario")]
        public string vchUsuario { get; set; }
        [Required(ErrorMessage = "* El Nombre esta vacío.")]
        [Display(Name = "Nombre")]
        public string vchNombre { get; set; }
        [Display(Name = "Activo")]
        public bool bitActivo { get; set; }
        [Required(ErrorMessage = "* Selecciones un Perfil.")]
        [Display(Name = "Perfil")]
        public int intPerfil { get; set; }
        [Display(Name = "Perfil")]
        public string vchPerfil { get; set; }

    }

    public class SP_GetUsuariosReporte
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }
        public string Perfil { get; set; }
    }
}