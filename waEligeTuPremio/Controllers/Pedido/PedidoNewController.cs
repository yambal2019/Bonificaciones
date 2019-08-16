using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using waEligeTuPremio.Data;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Pedido
{
    public class PedidoNewController : Controller
    {
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Inicio()
        {
            TBPremioModel obj = new TBPremioModel();
            using (var db = new DBPremioEntities())
            {

                obj.ListaNivelPremio = db.Database.SqlQuery<NivelPremio>("TBPremio_GetNiveles").ToList();
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"];

                obj.ListaPremio = DAOPremio.PremioPorIdConsultora(Convert.ToInt32(cookie.Value));

                var sql = "SELECT vchMensaje from TMensaje WHERE bitActivo = 1 and vchTipo = 'Mensaje Bienvenida'";
                string mensaje = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                ViewBag.mensaje = mensaje;

            }
            return View(obj);


        }




        [HttpPost]
        public JsonResult AgregarPedidoDetalle(int idPedido, Int32 idNivel, Int32 idPremio)
        {


            try
            {
                var respuesta = DAOPedidoDetalle.InsertPedidoDetalle(idPedido, idNivel, idPremio);




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new { Message = "Exito", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult Retorno()
        {
            try
            {

                var cookie = new HttpCookie("Cookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Value = string.Empty;
                Response.Cookies.Add(cookie);

                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            catch (Exception ex)
            {

                throw;
            }


            return Json(new { Message = "Exito", JsonRequestBehavior.AllowGet });
        }


    }
}