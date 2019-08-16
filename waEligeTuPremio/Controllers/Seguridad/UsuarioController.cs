using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;
using ClosedXML.Excel;

namespace waEligeTuPremio.Controllers.Seguridad
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private static string nombreUsr = null;
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult UsuarioIndex()
        {
            using (var db = new DBPremioEntities())
            {
                return View(db.Database.SqlQuery<SP_GetUsuario>("GetUsuario @vchNombre",
                    new SqlParameter("vchNombre", "")).ToList());
            }
        }

        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult UsuarioIndex(string nombre, string codigo)
        {
            using (var db = new DBPremioEntities())
            {
                if (nombre == null)
                {
                    if (nombreUsr == null)
                    {
                        nombre = "";
                        codigo = "";

                        return View(db.Database.SqlQuery<SP_GetUsuario>("GetUsuario @vchNombre, @vchUsuario",
                            new SqlParameter("vchNombre", nombre),
                            new SqlParameter("vchUsuario", codigo)
                            ).ToList());

                    }
                    else
                    {
                        codigo = "";
                        return View(db.Database.SqlQuery<SP_GetUsuario>("GetUsuario @vchNombre, @vchUsuario",
                            new SqlParameter("vchNombre", nombreUsr),
                            new SqlParameter("vchUsuario", codigo)).ToList());

                    }
                }
                else
                {

                    return View(db.Database.SqlQuery<SP_GetUsuario>("GetUsuario @vchNombre, @vchUsuario",
                        new SqlParameter("vchNombre", nombre),
                        new SqlParameter("vchUsuario", codigo)).ToList());

                }
            }
        }

        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Create()
        {
            using (var db = new DBPremioEntities())
            {
                List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                ViewBag.Perfil = ToSelectList(perfil);
            }

            ViewBag.Title = "Crear Usuario";

            return PartialView();
        }

        [NonAction]
        public SelectList ToSelectList(List<SP_Perfil> perfil)
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
            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public ActionResult Create(SP_GetUsuario usr)
        {

            using (var db = new DBPremioEntities())
            {
                List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                ViewBag.Perfil = ToSelectList(perfil);

                db.Database.ExecuteSqlCommand("InsertUsuario @vchUsuario, @vchNombre, @intPerfil",
                    new SqlParameter("vchUsuario", usr.vchUsuario),
                    new SqlParameter("vchNombre", usr.vchNombre),
                    new SqlParameter("intPerfil", usr.intPerfil));

                nombreUsr = usr.vchNombre;

                return Json(new { success = true });
            }

        }

        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Edit(int id = 0)
        {
            using (var db = new DBPremioEntities())
            {
                SP_GetUsuario banco = db.Database.SqlQuery<SP_GetUsuario>("GetUsuarioID @intCodigo",
                    new SqlParameter("intCodigo", id)).FirstOrDefault();

                List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                ViewBag.Perfil = ToSelectList(perfil);

                ViewBag.Title = "Editar Usuario";
                return PartialView(banco);
            }
        }

        [HttpPost]
        public ActionResult Edit(SP_GetUsuario usr)
        {
            using (var db = new DBPremioEntities())
            {
                //try
                //{
                List<SP_Perfil> perfil = db.Database.SqlQuery<SP_Perfil>("GetPerfil").ToList();
                ViewBag.Perfil = ToSelectList(perfil);

                db.Database.ExecuteSqlCommand("UpdateUsuario @intCodigo, @vchUsuario, @vchNombre, @bitActivo, @intPerfil",
                new SqlParameter("intCodigo", usr.intCodigo),
                new SqlParameter("vchUsuario", usr.vchUsuario),
                new SqlParameter("vchNombre", usr.vchNombre),
                new SqlParameter("bitActivo", usr.bitActivo),
                new SqlParameter("intPerfil", usr.intPerfil));

                nombreUsr = usr.vchNombre;

                return Json(new { success = true });

                //return RedirectToAction("UsuarioIndex", "Usuario", new { @nombre = usr.vchNombre });
                //}
                //catch (Exception)
                //{
                //    return View(usr);
                //    throw;
                //}
            }
        }

        public FileResult downloadFile()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();
                using (var db = new DBPremioEntities())
                {
                    List<SP_GetUsuariosReporte> lCalendario = db.Database.SqlQuery<SP_GetUsuariosReporte>("GetUsuarios").ToList();

                    dt = ToDataTable(lCalendario);
                }

                wb.Worksheets.Add(dt, "Calendario");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calendario_" +
                    DateTime.Now.ToString() + ".xlsx");
                }

            }

        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;

        }
    }
}