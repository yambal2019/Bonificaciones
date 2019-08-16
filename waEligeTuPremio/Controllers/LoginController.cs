using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using waEligeTuPremio.Models;
using waEligeTuPremio.srLoginYanbal;
using System.Data.SqlClient;

namespace waEligeTuPremio.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return RedirectToAction("LoginCNSIndex", "LoginCNS");
            //List<SP_GetPremio> premio = new List<SP_GetPremio>();
            //using (var db = new DBPremioEntities())
            //{
            //    premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioExiste").ToList();
            //}

            //ViewBag.CantPremio = premio.Count;

            //csLogin login = new csLogin
            //{
            //    Usuario = "",
            //    Contrasena = ""
            //};

            //return View(login);
        }
        //[HttpPost]
        ////[AllowAnonymous]
        //public ActionResult Index(csLogin user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            //creamos una clase de tipo cLoginWS que hace referencia al Web Service de corporación
        //            //llamamos al metodo de validar usuario, nos devuelve valores 0 ó 1;

        //            cLoginWS ws = new cLoginWS();
        //            Datos data = ws.getUsuario("3", user.Usuario, user.Contrasena).detalle.respuesta.datos;

        //            string login = "";
        //            if (data.flagValidacion == "0")
        //            {
        //                login = data.usuario;
        //                ModelState.AddModelError(string.Empty, "Datos de Usuario o Contraseña incorrectos.");
        //                return View(user);
        //            }

        //            GetUsuarioXUsuario gusr = new GetUsuarioXUsuario();
        //            UsuarioPerfilModel m = new UsuarioPerfilModel();

        //            m = gusr.GetUsuarioPerfil(user.Usuario);
        //            if (m == null)
        //            {
        //                ModelState.AddModelError(string.Empty, "Datos incorrectos. Usuario " + user.Usuario + " no registrado en el sistema.");
        //                return View(user);
        //            }

        //            List<SP_GetPremio> premio = new List<SP_GetPremio>();
        //            using (var db = new DBPremioEntities())
        //            {
        //                premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioExiste").ToList();
        //            }

        //            if (premio.Count == 0)
        //            {
        //                ModelState.AddModelError(string.Empty, "No existe ningún GANAMAS activo.");
        //                return View(user);

        //                //return RedirectToAction("Mensaje", "Login", new { @data_modal = "" });

        //            }

        //            FormsAuthentication.RedirectFromLoginPage(m.intCodigo.ToString(), true);

        //            // verificamos si ya finalizo el pedido
        //            int pedidoExiste = 0;
        //            using (var db = new DBPremioEntities())
        //            {
        //                pedidoExiste = db.Database.SqlQuery<int>("GetPedidoExisteUsr @intUsr",
        //                        new SqlParameter("intUsr", int.Parse(HttpContext.User.Identity.Name))).FirstOrDefault();
        //            }

        //            if (pedidoExiste > 0)
        //            {
        //                ViewBag.pedidoExiste = pedidoExiste;
        //                //user.JsFunction = "$( window ).load(function() { DisparaAlert();}); ";
        //                //return View(user);


        //                return JavaScript("ShowAlert(Prueba);");
        //            }
        //            else
        //            {
        //                return RedirectToAction("InicioPedidoIndex", "InicioPedido");
        //            }
        //            //93197
        //            //FormsAuthentication.RedirectFromLoginPage("93197", true);
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError(string.Empty, "Datos de Usuario o Contraseña incorrectos.");
        //            return View(user);
        //            throw;
        //        }
        //    }

        //    return View();
        //}

        //[HttpPost]
        public ActionResult IndexUrl(string id)
        {
            FormsAuthentication.RedirectFromLoginPage(id, true);
            return RedirectToAction("InicioPedidoIndex", "InicioPedido");
        }

        [HttpPost]
        public ActionResult ValidarIngreso(string usr, string password)
        {

            try
            {
                //creamos una clase de tipo cLoginWS que hace referencia al Web Service de corporación
                //llamamos al metodo de validar usuario, nos devuelve valores 0 ó 1;

                cLoginWS ws = new cLoginWS();
                Datos data = ws.getUsuario("3", usr, password).detalle.respuesta.datos;

                string login = "";
                if (data.flagValidacion == "0" || data.flagValidacion == null)
                {
                    login = data.usuario;
                    //ModelState.AddModelError(string.Empty, "Datos de Usuario o Contraseña incorrectos.");
                    return Json("Datos de Usuario o Contraseña incorrectos.");
                }

                GetUsuarioXUsuario gusr = new GetUsuarioXUsuario();
                UsuarioPerfilModel m = new UsuarioPerfilModel();

                m = gusr.GetUsuarioPerfil(usr);
                if (m == null)
                {
                    //ModelState.AddModelError(string.Empty, "Datos incorrectos. Usuario " + user.Usuario + " no registrado en el sistema.");
                    return Json("Datos incorrectos. Usuario " + usr + " no registrado en el sistema.");
                }

                List<SP_GetPremio> premio = new List<SP_GetPremio>();
                using (var db = new DBPremioEntities())
                {
                    premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioExiste").ToList();
                }

                if (premio.Count == 0)
                {
                    //ModelState.AddModelError(string.Empty, "No existe ningún GANAMAS activo.");
                    return Json("No existe ningún GANAMAS activo.");

                }

                // verificamos si ya finalizo el pedido
                int pedidoExiste = 0;
                using (var db = new DBPremioEntities())
                {
                    pedidoExiste = db.Database.SqlQuery<int>("GetPedidoExisteUsr @intUsr",
                            new SqlParameter("intUsr", int.Parse(m.intCodigo.ToString()))).FirstOrDefault();
                }

                if (pedidoExiste > 0)
                {
                    //ViewBag.pedidoExiste = pedidoExiste;
                    return Json("Querida Consultora Independiente ya elegiste tu premio de forma existosa, ¡espéralo en las próximas semanas!");
                }
                else
                {
                    //FormsAuthentication.RedirectFromLoginPage(m.intCodigo.ToString(), true);
                    //return RedirectToAction("InicioPedidoIndex", "InicioPedido");
                    return Json("ok," + m.intCodigo.ToString());
                }
                //93197
                //FormsAuthentication.RedirectFromLoginPage("93197", true);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Datos de Usuario o Contraseña incorrectos.");
                return Json("Datos de Usuario o Contraseña incorrectos.");
                throw;
            }
        }

        public ActionResult Mensaje()
        {
            
            ViewBag.Title = "Imagen";
            return View();
            
        }

    }
}