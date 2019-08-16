using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Pedido
{
    [Authorize]
    public class HistoricoController : Controller
    {
        // GET: Historico
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult HistoricoIndex()
        {
            using (var db = new DBPremioEntities())
            {
                //return View(db.Database.SqlQuery<SP_GetPedidoHistorico>("GetPedidoHistorico, @intUsr",
                //    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());

                return View(db.Database.SqlQuery<SP_GetPedidoHistorico>("GetPedidoHistorico @intUsr",
                      new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());

            }

        }

        public ActionResult Detalle(int id = 0)
        {
            using (var db = new DBPremioEntities())
            {
                ViewBag.Title = "Detalle de Pedido "; /*+ id;*/
                return PartialView(db.Database.SqlQuery<SP_GetPedidoDetalleHistorico>("GetPedidoDetalleHistorico @intPedido",
                    new SqlParameter("intPedido", id)).ToList());
            }
        }
    }
}