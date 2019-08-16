using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Models
{
    public class NavbarItem
    {
        public int Id { get; set; }
        public string nameOption { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public int parentId { get; set; }
        public bool isParent { get; set; }
        public string nameUsr { get; set; }
        public string VchUsuario { get; set; }
    }
    public class Navbar
    {
        public IEnumerable<NavbarItem> NavbarTop()
        {
            var topNav = new List<NavbarItem>();


            using (var db = new DBPremioEntities())
            {

                string name = HttpContext.Current.User.Identity.Name;

                UsuarioPerfilModel usr = new UsuarioPerfilModel();

                usr = db.Database.SqlQuery<UsuarioPerfilModel>("GetUsuarioToID @intCodigo",
                    new SqlParameter("intCodigo", name)).FirstOrDefault();

                if (usr == null)
                {
                    //HttpContext.Current.Response.Redirect("~/");
                    HttpContext.Current.Response.Redirect("~/Login/Index");
                    return topNav;
                }

                List<NavbarItem> menu = db.Database.SqlQuery<NavbarItem>("GetMenuPerfil @intPerfil",
                    new SqlParameter("intPerfil", usr.intPerfil)).ToList();
                foreach (var item in menu)
                {
                    item.nameUsr = usr.vchNombre;
                    item.VchUsuario = usr.vchUsuario;
                }

                foreach (var item in menu)
                {
                    topNav.Add(new NavbarItem()
                    {
                        Id = item.Id,
                        action = item.action,
                        nameOption = item.nameOption,
                        controller = item.controller,
                        isParent = item.isParent,
                        parentId = item.parentId,
                        nameUsr = item.nameUsr,
                        VchUsuario = item.VchUsuario
                    });
                }

            }

            return topNav;
        }
    }

    public class DownNavbarItem
    {
        public string mensaje { get; set; }
    }
    public class DownNavbar
    {
        public DownNavbarItem MensajeAJ()
        {
            using (var db = new DBPremioEntities())
            {
                var Down = new DownNavbarItem
                {
                    mensaje = ""
                };
                string sql = "";
                try
                {
                    sql = "SELECT vchMensaje from TMensaje WHERE bitActivo = 1 and vchTipo = 'Mensaje AJ'";
                    Down.mensaje = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                    return Down;
                }
                catch (Exception)
                {
                    return Down;
                }
            }
        }
    }
}