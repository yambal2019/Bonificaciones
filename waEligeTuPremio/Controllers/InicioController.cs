using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers
{
    [Authorize]
    public class InicioController : Controller
    {
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult InicioIndex()
        {
            using (var db = new DBPremioEntities())
            {
                return View(db.Database.SqlQuery<SP_GetPremio>("GetPremio").ToList());
            }
        }
    }
}