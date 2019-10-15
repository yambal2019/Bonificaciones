using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using waEligeTuPremio.Data;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Procesos
{
    public class ProcesoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            TBPremioModel obj = CargaDatosInicial();
            return View(obj);
            
        }

        public JsonResult GetCampaña(string AniosId)
        {

            return Json(new SelectList(DAOCampaña.ListaCampañasPorAño(AniosId), "Value", "Text", JsonRequestBehavior.AllowGet));

        }

        private  TBPremioModel CargaDatosInicial()
        {
            TBPremioModel obj = new TBPremioModel();
            List<SelectListItem> ListaCampaña = new List<SelectListItem>();
            ListaCampaña.Add(new SelectListItem { Text = "--Seleccione una campaña--", Value = "0" });
            obj.ListaCampaña = new SelectList(ListaCampaña, "Value", "Text");
            obj.ListaNivelPremio = new List<NivelPremio>();
            obj.ListaPremio = new List<TBPremioModel>();
            obj.ListaAnios = new SelectList(DAOCampaña.ListaAñios(), "Value", "Text");
            return obj;
        }



        [HttpPost]
        public ActionResult Index(TBPremioModel model)
        {
            return View();
        }
    }
}