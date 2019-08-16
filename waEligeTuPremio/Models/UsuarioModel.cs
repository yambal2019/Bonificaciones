using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "*")]
        public string Login { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
    }

    public class UsuarioPerfilModel
    {
        public int intCodigo { get; set; }
        public string vchUsuario { get; set; }
        public string vchNombre { get; set; }
        public int intPerfil { get; set; }
        public string Perfil { get; set; }
    }
    public class GetUsuarioXUsuario
    {
        public UsuarioPerfilModel GetUsuarioPerfil(string usuario)
        {
            UsuarioPerfilModel usr = new UsuarioPerfilModel();
            using (var db = new DBPremioEntities())
            {
                usr = db.Database.SqlQuery<UsuarioPerfilModel>("GetUsuarioToUsuario @vchUsuario",
                    new SqlParameter("vchUsuario", usuario)).FirstOrDefault();
            }
            return usr;
        }
    }
}