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
    public class InicioPedidoController : Controller
    {
        // GET: InicioPedido
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult InicioPedidoIndex()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {

                    db.Database.ExecuteSqlCommand("DeletePedidoTemp @intUsr",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)));

                    //obtenemos el nombre de la consultora
                    var sql = "SELECT vchNombre FROM dbo.TUsuario Where intCodigo = " + int.Parse(HttpContext.User.Identity.Name);
                    string nombreCNS = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                    ViewBag.Consultora = nombreCNS;

                    //obtenemos los puntos de disponibles
                    int puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).FirstOrDefault();
                    ViewBag.puntoDisponibles = puntoDisponibles;

                    // obtenemos el mensaje para la consultora.
                    try
                    {
                        sql = "SELECT vchMensaje from TMensaje WHERE bitActivo = 1 and vchTipo = 'Mensaje Bienvenida'";
                        string mensaje = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                        ViewBag.mensaje = mensaje;
                    }
                    catch (Exception)
                    {
                        ViewBag.mensaje = "";
                    }

                    // verificamos si ya finalizo el pedido
                    int pedidoExiste = db.Database.SqlQuery<int>("GetPedidoExisteUsr @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).FirstOrDefault();
                    ViewBag.pedidoExiste = pedidoExiste;

                    return View(db.Database.SqlQuery<SP_GetPremio>("GetPremioConsultora").ToList());
                }
            }
            catch (Exception)
            {
                using (var db = new DBPremioEntities())
                {
                    List<SP_GetPremio> premioError = new List<SP_GetPremio>();
                    return View(premioError);
                }
            }
        }
    }
}