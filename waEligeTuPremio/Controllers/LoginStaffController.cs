using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using waEligeTuPremio.Models;
using waEligeTuPremio.srLoginYanbal;

namespace waEligeTuPremio.Controllers
{
    public class LoginStaffController : Controller
    {
        public ActionResult LoginStaff()
        {
            csLogin login = new csLogin();
            login.Usuario = "";
            login.Contrasena = "";
            return View(login);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginStaff(csLogin user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //creamos una clase de tipo cLoginWS que hace referencia al Web Service de corporación
                    //llamamos al metodo de validar usuario, nos devuelve valores 0 ó 1;
                    cLoginWS ws = new cLoginWS();
                    Datos data = ws.getUsuario("1", user.Usuario, user.Contrasena).detalle.respuesta.datos;

                    //Todo:
                    user.Usuario = "dgaza";
                    data.flagValidacion = "1";

                    string login = "";
                    if (data.flagValidacion == "0")
                    {
                        login = data.usuario;
                        ModelState.AddModelError(string.Empty, "Datos de Usuario o Contraseña incorrectos.");
                        return View(user);
                    }

                    // una vez que el usuario exista en el active directory
                    // obtenemos los datos del usuario de la base de datos para 
                    // poder habilitar segun el usuario las opciones en el sistema.
                    GetUsuarioXUsuario gusr = new GetUsuarioXUsuario();
                    UsuarioPerfilModel m = new UsuarioPerfilModel();

                    m = gusr.GetUsuarioPerfil(user.Usuario);
                    if (m == null)
                    {
                        ModelState.AddModelError(string.Empty, "Datos incorrectos. Usuario " + user.Usuario + " no registrado en el sistema.");
                        return View(user);
                    }
                    FormsAuthentication.RedirectFromLoginPage(m.intCodigo.ToString(), true);
                    return RedirectToAction("InicioIndex", "Inicio");

                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Datos de Usuario o Contraseña incorrectos.");
                    return View(user);
                    throw;
                }
            }

            return View();
        }
    }
}