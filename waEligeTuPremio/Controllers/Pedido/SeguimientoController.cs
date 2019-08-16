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
    public class SeguimientoController : Controller
    {
        // GET: Seguimiento
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult SeguimientoIndex()
        {
            using (var db = new DBPremioEntities())
            {
                List<SP_GetListaPlan> plan = db.Database.SqlQuery<SP_GetListaPlan>("GetListaPlan").ToList();
                ViewBag.Plan = ToSelectList(plan, "1");

                List<SP_GetListaPedidoDir> listaDir = new List<SP_GetListaPedidoDir>();

                return View(listaDir);
            }
        }

        [HttpPost]
        public ActionResult SeguimientoIndex(SP_GetListaPedidoDir plan)
        {

            //TRAEMOS TODAS LAS CNS QUE FINALIZARON SU PEDIDO SEGUN EL PLAN
            using (var db = new DBPremioEntities())
            {
                List<SP_GetListaPlan> Lplan = db.Database.SqlQuery<SP_GetListaPlan>("GetListaPlan").ToList();
                ViewBag.Plan = ToSelectList(Lplan, "1");

                if (plan.intCodigo == 0)
                {
                    List<SP_GetListaPedidoDir> listaDir = new List<SP_GetListaPedidoDir>();

                    return View(listaDir);
                }
                else
                {
                    string nombPlan = db.Database.SqlQuery<string>("SELECT vchPlan FROM TPremio WHERE intCodigo = " + plan.intCodigo).FirstOrDefault();

                    return View(db.Database.SqlQuery<SP_GetListaPedidoDir>("GetListaPedidoDir @intUsr, @vchPlan",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)),
                            new SqlParameter("vchPlan", nombPlan)
                            ).ToList());

                }

            }

        }

        [NonAction]
        public SelectList ToSelectList(List<SP_GetListaPlan> perfil, string SelectedID)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in perfil)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.vchPlan,
                    Value = item.intCodigo.ToString(),
                });
            }
            return new SelectList(list, "Value", "Text", SelectedID);
        }
    }
}