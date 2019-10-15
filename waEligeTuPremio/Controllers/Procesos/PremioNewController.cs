using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using waEligeTuPremio.Data;
using waEligeTuPremio.Models;


namespace waEligeTuPremio.Controllers.Procesos
{
    public class PremioNewController : Controller
    {

      
        [HttpGet]
        public ActionResult Index()
        {
            TBPremioModel obj = CargaDatosInicial();

            if (TempData["Error"]!= null)
            {
                ViewBag.MensajeError = TempData["Error"].ToString();
            }
            
            return View(obj);
        }

        private static TBPremioModel CargaDatosInicial()
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

        public JsonResult GetCampaña(string AniosId)
        {
        
            return Json(new SelectList(DAOCampaña.ListaCampañasPorAño(AniosId), "Value", "Text", JsonRequestBehavior.AllowGet));

        }


        public ActionResult Busqueda(TBPremioModel objModel)
        {
            TBPremioModel obj = new TBPremioModel();

            obj.ListaPremio = DAOPremio.ListaPremioPorCampaña(objModel.SelectedCampañaId);
            obj.ListaNivelPremio = DAOPremio.ListaNivelPorCampaña(objModel.SelectedCampañaId);

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
            obj.SelectedCampañaId = objModel.SelectedCampañaId;


            if (Request.IsAjaxRequest())
            {
                return PartialView("_DetallePremio", obj);
            }


            return View("Index", obj);
        }

        [HttpPost]
        public ActionResult UploadImg()
        {

            if (Request.Files.Count > 0)
            {
                //try
                //{
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase archivo = files[0];

                if (archivo == null || archivo.ContentLength == 0)
                {
                    return Json("Seleccione una Imagen.");
                }
                else
                {
                    if (archivo.FileName.EndsWith("jpg") || archivo.FileName.EndsWith("jpeg") || archivo.FileName.EndsWith("png"))
                    {
                        byte[] byteImg = null;
                        using (BinaryReader br = new BinaryReader(archivo.InputStream))
                        {
                            byteImg = br.ReadBytes(archivo.ContentLength);
                            br.BaseStream.Read(byteImg, 0, archivo.ContentLength);
                        }


                        string intPremio = Request.Form["intPremio"].ToString();
                        Int32 SelectedCampañaId = Convert.ToInt32( Request.Form["intCampana"]);

                        TBImage objImage = new TBImage();
                        objImage.vchNombre = archivo.FileName.ToString();
                        objImage.vchImagen = byteImg;
                        objImage.intPremio = Convert.ToInt32(intPremio);
                        objImage.vchExtencion = Path.GetExtension(archivo.FileName);

                        DAOImage.Update(objImage);


                    };
                    
                }
            }
         
            TBPremioModel obj = CargaDatosInicial();
            return View("Index",obj);
     
        }

        [HttpPost]
        public ActionResult PremioNuevoPartial()
        {
            TBPremioModel obj = CargaDatosInicial();
            return PartialView("PremioNuevoPartial", obj);
        }


        [HttpPost]
        public ActionResult PremioGuardarPartial( TBPremioModel model)
        {

            try
            {
               
                if (model.intPremio > 0)
                {
                    model.intCampaña = model.SelectedCampañaNuevoEditarId;
                    DAOPremio.Update(ref model);

                    if (model.Error!= null)
                    {
                        TempData["Error"] = model.Error;

                    }
                    
                }
                
                else
                {
                    DAOPremio.Add(model);
                }

            }
            catch (Exception ex)
            {
               
                return RedirectToAction("Index", "PremioNew");
            }

         
            return RedirectToAction("Index", "PremioNew");
        }



        [HttpPost]
        public ActionResult PremioEditarPartial(Int32 intPremio)
        {
            TBPremioModel obj = new TBPremioModel();
            obj = DAOPremio.PremioPorIdPremio(intPremio);


            obj.ListaAnios = new SelectList(DAOCampaña.ListaAñios(), "Value", "Text");
            obj.AniosNuevoEditarId = Convert.ToInt32(obj.AniosNuevoEditarId);


            obj.ListaCampaña = new SelectList(DAOCampaña.ListaCampañasPorAño(obj.AniosNuevoEditarId.ToString()), "Value", "Text");
            obj.SelectedCampañaNuevoEditarId = Convert.ToInt32(obj.intCampaña);


            return PartialView("PremioEditarPartial", obj);


        }


        [HttpPost]
        public ActionResult PremioEliminarPartial(Int32 intPremio)
        {
            TBPremioModel obj = new TBPremioModel();
            obj = DAOPremio.PremioPorIdPremio(intPremio);

            obj.ListaAnios = new SelectList(DAOCampaña.ListaAñios(), "Value", "Text");
            obj.AniosNuevoEditarId = Convert.ToInt32(obj.AniosNuevoEditarId);


            obj.ListaCampaña = new SelectList(DAOCampaña.ListaCampañasPorAño(obj.AniosNuevoEditarId.ToString()), "Value", "Text");
            obj.SelectedCampañaNuevoEditarId = Convert.ToInt32(obj.intCampaña);

            return PartialView("PremioEliminarPartial", obj);


        }
        [HttpPost]
        public ActionResult PremioEliminar(TBPremioModel model)
        {
            TBPremioModel obj = new TBPremioModel();
            DAOPremio.PremioEliminar(model.intPremio);
            return RedirectToAction("Index", "PremioNew");
        }
    }
}