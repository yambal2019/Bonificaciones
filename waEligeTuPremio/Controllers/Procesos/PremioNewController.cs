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

        private IEnumerable<Añio> GetAllAnios()
        {
            Int32 valor = Convert.ToInt32( ConfigurationManager.AppSettings["AñoFinal"]);

            List<Añio> objLista = new List<Añio>();


            for (int i = 2018; i <= valor; i++)
            {
                //ListaItems(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                Añio obj = new Añio();
                obj.id = i.ToString();
                obj.Anio = i.ToString();
                objLista.Add(obj);
            }

         
            return objLista;
        }


        public ActionResult Index()
        {
            TBPremioModel obj = NewMethod();

            return View(obj);
        }

        private static TBPremioModel NewMethod()
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


        public ActionResult Busqueda(Int32 SelectedCampañaId)
        {
            TBPremioModel obj = new TBPremioModel();
            obj.ListaPremio = DAOPremio.ListaPremioPorCampaña(SelectedCampañaId);
            obj.ListaNivelPremio = DAOPremio.ListaNivelPorCampaña(SelectedCampañaId);

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
            obj.SelectedCampañaId = SelectedCampañaId;


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

                        TBImage objImage = new TBImage();
                        objImage.vchNombre = archivo.FileName.ToString();
                        objImage.vchImagen = byteImg;
                        objImage.intPremio = Convert.ToInt32(intPremio);
                        objImage.vchExtencion = Path.GetExtension(archivo.FileName);

                        DAOImage.Update(objImage);


                        //            return Json("Status de Importacion: Se subio correctamente la imagen.");
                        //        }
                        //        else
                        //        {
                        //            return Json("Tipo incorrecto de Archivo.");
                        //        }
                    };
                    //}
                    //catch (Exception ex)
                    //{
                    //    return Json("Error occurred. Error details: " + ex.Message);
                    //}
                }
            }
            //TBPremioModel obj = new TBPremioModel();
            ////List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();

            ////obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");

            ////return View("Index", obj);
            ////TBPremioModel obj = new TBPremioModel();
            //////DAOPremio.PremioEliminar(model.intPremio);
            //// obj.ListaNivelPremio = DAOPremio.ListaNivelPorCampaña(SelectedCampañaId);

            ////TBPremioModel obj = new TBPremioModel();

            //List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            //obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
            TBPremioModel obj = NewMethod();
            // return RedirectToAction("Index", "PremioNew");
            return View("Index",obj);
          //  return View();
        }

        [HttpPost]
        public ActionResult PremioNuevoPartial()
        {
            TBPremioModel obj = new TBPremioModel();

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");

            return PartialView("PremioNuevoPartial", obj);
        }


        [HttpPost]
        public ActionResult PremioGuardarPartial(TBPremioModel model)
        {

            TBPremioModel obj = new TBPremioModel();
            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();

            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
        


            try
            {
               
                if (model.intPremio > 0)
                {
                    model.intCampaña = model.SelectedCampañaId;
                    DAOPremio.Update(model);
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
            //obj.ListaNivelPremio = DAOPremio.ListaNivelPorCampaña(SelectedCampañaId);

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
            obj.SelectedCampañaId = Convert.ToInt32(obj.intCampaña);


            return PartialView("PremioEditarPartial", obj);


        }


        [HttpPost]
        public ActionResult PremioEliminarPartial(Int32 intPremio)
        {
            TBPremioModel obj = new TBPremioModel();
            obj = DAOPremio.PremioPorIdPremio(intPremio);
            //obj.ListaNivelPremio = DAOPremio.ListaNivelPorCampaña(SelectedCampañaId);

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
            obj.SelectedCampañaId = Convert.ToInt32(obj.intCampaña);


            return PartialView("PremioEliminarPartial", obj);


        }
        [HttpPost]
        public ActionResult PremioEliminar(TBPremioModel model)
        {
            TBPremioModel obj = new TBPremioModel();
            DAOPremio.PremioEliminar(model.intPremio);
            //obj.ListaNivelPremio = DAOPremio.ListaNivelPorCampaña(SelectedCampañaId);

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
            //obj.SelectedCampañaId = Convert.ToInt32(obj.intCampaña);


            //return PartialView("PremioEliminarPartial", obj);

            //return View("Index",);
            return RedirectToAction("Index", "PremioNew");
        }
    }
}