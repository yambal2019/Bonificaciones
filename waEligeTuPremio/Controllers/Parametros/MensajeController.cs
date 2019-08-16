using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Parametros
{
    [Authorize]
    public class MensajeController : Controller
    {
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        // GET: Mensaje
        public ActionResult MensajeIndex()
        {

            using (var db = new DBPremioEntities())
            {
                SP_GetMensaje mensajeCant = new SP_GetMensaje();

                List<SP_GetMensaje> mensaje = db.Database.SqlQuery<SP_GetMensaje>("GetMensaje").ToList();
                mensajeCant = db.Database.SqlQuery<SP_GetMensaje>("GetMensajeActivoCant").FirstOrDefault();
                mensaje.ElementAt(0).Cantidad = mensajeCant.Cantidad;

                //if (mensajeCant.Cantidad > 1)
                //{
                //    ViewBag.cantidad = "Solo se Permite un mensaje Activo. Seleccione solo uno.";
                //}

                return View(mensaje);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Crear Mensaje";

            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(SP_GetMensaje ms)
        {

            using (var db = new DBPremioEntities())
            {
                db.Database.ExecuteSqlCommand("InsertMensaje @vchMensaje, @intUsr",
                    new SqlParameter("vchMensaje", ms.vchMensaje),
                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)));

                return Json(new { success = true });
            }

        }

        public ActionResult Edit(int id = 0)
        {
            using (var db = new DBPremioEntities())
            {
                SP_GetMensaje mensaje = db.Database.SqlQuery<SP_GetMensaje>("GetMensajeID @intCodigo",
                    new SqlParameter("intCodigo", id)).FirstOrDefault();

                ViewBag.Title = "Editar Mensaje";
                return PartialView(mensaje);
            }
        }

        [HttpPost]
        public ActionResult Edit(SP_GetMensaje ms)
        {
            using (var db = new DBPremioEntities())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("UpdateMensaje @intCodigo, @vchMensaje, @bitActivo",
                    new SqlParameter("intCodigo", ms.intCodigo),
                    new SqlParameter("vchMensaje", ms.vchMensaje),
                    new SqlParameter("bitActivo", ms.BitActivo));

                    return Json(new { success = true });
                }
                catch (Exception)
                {
                    return Json(new { success = false });
                    throw;
                }

            }
        }

        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        //public ActionResult MensajeActivo(bool BitActivo)
        //{
        //    var validUserName = true;
        //    if (BitActivo)
        //    {

        //        SP_GetMensaje mensaje = new SP_GetMensaje();
        //        using (var db = new DBPremioEntities())
        //        {
        //            mensaje = db.Database.SqlQuery<SP_GetMensaje>("GetMensajeActivoCant").FirstOrDefault();
        //        }
        //        if (mensaje != null)
        //            validUserName = mensaje.Cantidad == 0;

        //        return Json(validUserName, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Json(validUserName, JsonRequestBehavior.AllowGet);
        //}
    }
}