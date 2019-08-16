using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using waEligeTuPremio.Models;
using waEligeTuPremio.srLoginYanbal;
using System.Data.SqlClient;
using System.Web.Security;
using waEligeTuPremio.Data;

namespace waEligeTuPremio.Controllers
{
    public class LoginCNSController : Controller
    {
        // GET: LoginCNS
        public ActionResult LoginCNSIndex()
        {
            List<SP_GetPremio> premio = new List<SP_GetPremio>();
            using (var db = new DBPremioEntities())
            {
                premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioExiste").ToList();

            }

            ViewBag.CantPremio = premio.Count;

            csLogin login = new csLogin
            {
                Usuario = "",
                Contrasena = ""
            };

            return View(login);
        }


        public ActionResult IndexUrl(string id)
        {
            FormsAuthentication.RedirectFromLoginPage(id, true);
            return RedirectToAction("Inicio", "PedidoNew");
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

                //  string login = "";
                if (data.flagValidacion == "0" || data.flagValidacion == null)
                {
                
                    return Json("Datos de Usuario o Contraseña incorrectos. Intenta nuevamente ");
                }

                //si el usuario no existe ingresa en la tabla TConsultora 

                TConsultora objTConsultora = new TConsultora();
                objTConsultora.intCodigoCNS = data.usuario;
                objTConsultora.vchNombreCompleto = data.nombreCompleto;
                objTConsultora.vchEmail = data.email;

                String[] valores = new String[4];

                valores = data.nombreCompleto.Split(' ');
                string n1, n2, n3, n4;

                if (valores.Length == 3)
                {
                    n1 = valores[0] ?? "";
                    n2 = "";
                    n3 = valores[1] ?? "";
                    n4 = valores[2] ?? "";
                }
                else
                {
                    n1 = valores[0] ?? "";
                    n2 = valores[1] ?? "";
                    n3 = valores[2] ?? "";
                    n4 = valores[3] ?? "";
                }


                objTConsultora.vchNombre = n1 + ' ' + n2;
                objTConsultora.vchApellido = n3 + ' ' + n4;

                DAOConsultora.InsertConsultoraNueva(objTConsultora);

                GetUsuarioXUsuario gusr = new GetUsuarioXUsuario();
                UsuarioPerfilModel m = new UsuarioPerfilModel();

                m = gusr.GetUsuarioPerfil(usr);
                if (m == null)
                {
                    
                    return Json("Datos incorrectos. " + usr + " Usuario no habilitado para la sección ELIGE TU PREMIO.");
                }


                IList<TBPedidoModel> objPedido = DAOPedido.SelectPorIdConsultora(Convert.ToInt32(usr));

                if (objPedido != null && objPedido.Count > 0)
                {
                    // traer Informacion de Pedido.


                }
                else
                {
                    Int32 respuesta = DAOPedido.InsertPedido_DetallePedido(Convert.ToInt32(usr));
                }



                HttpCookie cookie = new HttpCookie("Cookie");
                cookie.Value = usr;

                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);


                return Json("ok," + m.intCodigo.ToString());

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Datos de Usuario o Contraseña incorrectos.");
                return Json("Datos de Usuario o Contraseña incorrectos.");
                throw;
            }
        }
    }


}