using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class SP_MenuIDPerfil
    {
        [Key]
        public int intID { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "* El Menu esta vacío.")]
        [Display(Name = "Menu")]
        public string nameOption { get; set; }
        public int parentId { get; set; }
        [Display(Name = "Acceso")]
        public bool bitAcceso { get; set; }
        public int intPerfil { get; set; }
        public Int64 Nro { get; set; }
    }
}