using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Seguridad
{
    [Authorize]
    public class PerfilController : Controller
    {
        private static string idAuxPerfil = null;
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        // GET: Perfil
        public ActionResult PerfilIndex()
        {
            using (var db = new DBPremioEntities())
            {
                List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                ViewBag.Perfil = ToSelectList(perfil, "1");

                return View(db.Database.SqlQuery<SP_MenuIDPerfil>("GetMenuIDPerfil @intPerfil",
                    new SqlParameter("intPerfil", 1)).ToList());
            }
        }

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult PerfilIndex(string PerfilId)
        {
            using (var db = new DBPremioEntities())
            {
                if (PerfilId == null)
                {
                    if (idAuxPerfil == null)
                    {
                        PerfilId = "1";

                        List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                        ViewBag.Perfil = ToSelectList(perfil, PerfilId);

                        return View(db.Database.SqlQuery<SP_MenuIDPerfil>("GetMenuIDPerfil @intPerfil",
                            new SqlParameter("intPerfil", int.Parse(PerfilId))).ToList());
                    }
                    else
                    {
                        List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                        ViewBag.Perfil = ToSelectList(perfil, idAuxPerfil);

                        return View(db.Database.SqlQuery<SP_MenuIDPerfil>("GetMenuIDPerfil @intPerfil",
                            new SqlParameter("intPerfil", int.Parse(idAuxPerfil))).ToList());
                    }
                }
                else
                {
                    List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                    ViewBag.Perfil = ToSelectList(perfil, PerfilId);

                    return View(db.Database.SqlQuery<SP_MenuIDPerfil>("GetMenuIDPerfil @intPerfil",
                        new SqlParameter("intPerfil", int.Parse(PerfilId))).ToList());
                }

            }
        }

        public ActionResult Edit(int id = 0)
        {
            using (var db = new DBPremioEntities())
            {
                SP_MenuIDPerfil menu = db.Database.SqlQuery<SP_MenuIDPerfil>("GetMenuID @intID",
                    new SqlParameter("intID", id)).FirstOrDefault();

                ViewBag.Title = "Editar Menu";
                return PartialView(menu);
            }
        }

        [HttpPost]
        public ActionResult Edit(SP_MenuIDPerfil menu)
        {
            using (var db = new DBPremioEntities())
            {
                db.Database.ExecuteSqlCommand("UpdateMenuPerfil @intID, @bitAcceso",
                    new SqlParameter("intID", menu.intID),
                    new SqlParameter("bitAcceso", menu.bitAcceso));

                idAuxPerfil = menu.intPerfil.ToString();
                return Json(new { success = true });
            }
        }

        [NonAction]
        public SelectList ToSelectList(List<SP_Perfil> perfil, string SelectedID)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in perfil)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.vchNombre,
                    Value = item.intCodigo.ToString(),
                });
            }
            return new SelectList(list, "Value", "Text", SelectedID);
        }
    }
}