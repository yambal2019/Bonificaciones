using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Pedido
{
    [Authorize]
    public class PedidoController : Controller
    {
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        // public ActionResult PedidoIndex()
        // {
        //     using (var db = new DBPremioEntities())
        //     {
        //         return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
        //                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
        //     }
        // }

        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult PedidoIndex(int sap = 0, int pto = 0)
        {
            using (var db = new DBPremioEntities())
            {

                // verificamos si ya finalizo el pedido
                int pedidoExiste = db.Database.SqlQuery<int>("GetPedidoExisteUsr @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                ViewBag.pedidoExiste = pedidoExiste;


                int puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                ViewBag.puntoDisponibles = puntoDisponibles;

                SP_GetPedidoTemp pedido = db.Database.SqlQuery<SP_GetPedidoTemp>("GetPedidoTemp @intUsr",
                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).FirstOrDefault();
                if (pedido == null && sap == 0)
                {
                    db.Database.ExecuteSqlCommand("InsertPedidoTemp @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)));

                    return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                        new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                }
                else
                {
                    if (pedido == null && sap != 0)
                    {
                        if (puntoDisponibles >= 0)
                        {
                            int exite = db.Database.SqlQuery<int>("GetExistePremio @intUsr, @SAP",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)),
                            new SqlParameter("SAP", sap)
                            ).FirstOrDefault();

                            if (exite > 0)
                            {
                                puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                                ViewBag.puntoDisponibles = puntoDisponibles;

                                return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                            }
                            else
                            {
                                db.Database.ExecuteSqlCommand("InsertPedidoMasDetalleTemp @intUsr, @intCodigoSAP, @smintCantidad",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)),
                                new SqlParameter("intCodigoSAP", sap),
                                new SqlParameter("smintCantidad", 1));

                                puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                                ViewBag.puntoDisponibles = puntoDisponibles;

                                return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                            }

                        }
                        else
                        {
                            puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                            ViewBag.puntoDisponibles = puntoDisponibles;

                            return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                        }

                    }
                    else
                    {
                        if (pedido != null && sap == 0)
                        {
                            puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                            ViewBag.puntoDisponibles = puntoDisponibles;

                            return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                        }
                        else
                        {
                            if (puntoDisponibles - pto >= 0)
                            {
                                int exite = db.Database.SqlQuery<int>("GetExistePremio @intUsr, @SAP",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)),
                                new SqlParameter("SAP", sap)
                                ).FirstOrDefault();

                                if (exite > 0)
                                {
                                    puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                                    ViewBag.puntoDisponibles = puntoDisponibles;

                                    return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                                        new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                                }
                                else
                                {
                                    db.Database.ExecuteSqlCommand("InsertPedidoDetalleTemp @intUsr, @intCodigoSAP, @smintCantidad",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)),
                                    new SqlParameter("intCodigoSAP", sap),
                                    new SqlParameter("smintCantidad", 1));

                                    puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                                    ViewBag.puntoDisponibles = puntoDisponibles;

                                    return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                                        new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                                }
                            }
                            else
                            {
                                puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();
                                ViewBag.puntoDisponibles = puntoDisponibles;

                                return View(db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPedidoDetalleTemp @intUsr",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).ToList());
                            }

                        }

                    }

                }
            }
        }

        public ActionResult Delete(int sap = 0) //ActionResult
        {
            using (var db = new DBPremioEntities())
            {
                db.Database.ExecuteSqlCommand("DeletePedidoDetalleTemp @intUsr, @intCodigoSAP",
                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)),
                    new SqlParameter("intCodigoSAP", sap));

                //return Json("El Premio se eliminó correctamente.");

                return RedirectToAction("PedidoIndex");

            }
        }

        [HttpPost]
        public ActionResult FinalizarPedido()
        {

            try
            {
                using (var db = new DBPremioEntities())
                {
                    int puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).First();

                    if (puntoDisponibles != 0)
                    {
                        return Json("Aún no seleccionaste el premio que deseas. Debes escoger UNA OPCIÓN para poder presionar el botón FINALIZAR PEDIDO.");
                    }
                    else
                    {
                        SP_GetPedidoDetalleTemp premio = new SP_GetPedidoDetalleTemp();
                        premio = db.Database.SqlQuery<SP_GetPedidoDetalleTemp>("GetPremioStockDisponible @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).FirstOrDefault();

                        if (premio != null)
                        {
                            return Json("Stock insuficiente para el Producto " + premio.intCodigoCorto + " - " + premio.vchDescripcion);
                        }
                        else
                        {
                            db.Database.ExecuteSqlCommand("InsertPedido @intUsr",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)));


                            Thread t = new Thread(new ParameterizedThreadStart(EnviarCorreo));
                            t.Start(int.Parse(HttpContext.User.Identity.Name));

                            return Json("¡Elegiste tu premio! Tu pedido se guardó correctamente, revisa tu correo electrónico.");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return Json("No se puedo Finalizar el Pedido de Premios.");
                throw;
            }

        }

        [NonAction]
        public void EnviarCorreo(object usr)
        {
            using (var db = new DBPremioEntities())
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/HtmlTemplates/HPRespuestaPedido.html")))
                {
                    body = reader.ReadToEnd();
                }

                string codigoUsr = usr.ToString();
                SP_GetPedidoMail mail = new SP_GetPedidoMail();
                mail = db.Database.SqlQuery<SP_GetPedidoMail>("GetPedidoMail @intUsr",
                            new SqlParameter("intUsr", int.Parse(codigoUsr))).FirstOrDefault();

                //string premioUrl = mail.VchGanamas.ToString().Replace(' ', '-');
                //premioUrl = premioUrl+"-"+mail.
                //mail.VchPremio.ToString().Replace(' ', '-');
                //premioUrl = premioUrl.Remove(0, 1);
                //premioUrl = premioUrl.Remove(premioUrl.Length-1, 1);
                //string ganamasUrl = mail.VchGanamas.ToString().Replace(' ', '-');
                //premioUrl = premioUrl + "-" + ganamasUrl;


                body = body.Replace("{UserName}", mail.VchNombreCompleto);
                body = body.Replace("{nroPedido}", mail.IntPedido.ToString());
                body = body.Replace("{fechaPedido}", mail.DttmFecha.ToString());
                body = body.Replace("{direccionL1}", mail.VchDireccionL1);
                body = body.Replace("{distrito}", mail.VchDistrito);
                body = body.Replace("{ciudad}", mail.VchCiudad);
                body = body.Replace("{departamento}", mail.VchDepartamento);
                body = body.Replace("{premio}", mail.VchPremio);
                body = body.Replace("{ganamas}", mail.VchGanamas);
                //body = body.Replace("{imagen}", Convert.ToBase64String(mail.imgImagen));
                body = body.Replace("{mensajeAj}", mail.VchMensaje);
                body = body.Replace("{codConsultora}", mail.CodConsultora);
                body = body.Replace("{email}", mail.VchEmail);
                body = body.Replace("{premioUrl}", mail.VchGanamasNivelSAPTitulo);

                Utilities u = new Utilities();
                u.EnviarCorreo(mail.VchEmail, "Elección de premio recibida", body);
            }
        }

        [HttpPost]
        public ActionResult AddPremio()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    int puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                            new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).FirstOrDefault();

                    if (puntoDisponibles > 0)
                    {
                        return Json("ok");
                    }
                    else
                    {
                        return Json("No se puede Adicionar mas premios, no tiene Puntos disponibles.");
                    }
                }
            }
            catch (Exception)
            {
                return Json("No se puedo Adicionar Premios.");
                throw;
            }
        }

        [HttpPost]
        public ActionResult UpdatePremioCant(int sap = 0, int cant = 1, int cantServidor = 1)
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    int puntoDisponibles = db.Database.SqlQuery<int>("GetPuntosDisponibles @intUsr",
                                new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).FirstOrDefault();

                    SP_GetPremio premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioSAP @intCodigoSAP",
                                new SqlParameter("intCodigoSAP", sap)).FirstOrDefault();

                    int subTotal = (premio.smintPuntos * cant) - (premio.smintPuntos * cantServidor);

                    if (puntoDisponibles - subTotal < 0)
                    {
                        return Json("No se puede Adicionar esa cantidad, Puntos insuficientes.");
                    }

                    var sql = "SELECT smintStockrReal FROM TPremio WHERE intCodigoSAP = " + sap;
                    Int16 stockReal = db.Database.SqlQuery<Int16>(sql).FirstOrDefault();


                    if (stockReal - cant < 0)
                    {
                        return Json("No se puede Adicionar esa cantidad, Stock insuficiente.");
                    }

                    db.Database.ExecuteSqlCommand("UpdatePedidoDetalleTempCant @intUsr, @intCodigoSAP, @smintCantidad",
                                    new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name)),
                                    new SqlParameter("intCodigoSAP", sap),
                                    new SqlParameter("smintCantidad", cant));

                    return Json("ok");
                }
            }
            catch (Exception ex)
            {
                return Json("No se puedo Adicionar Premios.");
                throw;
            }
        }
    }
}