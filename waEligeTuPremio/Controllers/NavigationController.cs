using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult TopNav()
        {
            var nav = new Navbar();
            return PartialView("TopNav", nav.NavbarTop());
        }
        [Authorize]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("LoginCNSIndex", "LoginCNS");
        }
    }
}