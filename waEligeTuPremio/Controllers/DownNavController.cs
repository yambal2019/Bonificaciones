using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers
{
    public class DownNavController : Controller
    {
        // GET: DownNav
        public ActionResult DownNav()
        {
            var nav = new DownNavbar();
            return PartialView("DownNav", nav.MensajeAJ());
        }
    }
}