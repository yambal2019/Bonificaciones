using System;
using System.Collections.Generic;
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

        public ActionResult Index()
        {

            TBPremioModel obj = new TBPremioModel();
            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();

            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
            obj.ListaNivelPremio = new List<NivelPremio>();
            obj.ListaPremio = new List<TBPremioModel>();

            return View(obj);
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
            TBPremioModel obj = new TBPremioModel();
            //List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();

            //obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");

            //return View("Index", obj);
            //TBPremioModel obj = new TBPremioModel();
            ////DAOPremio.PremioEliminar(model.intPremio);
            // obj.ListaNivelPremio = DAOPremio.ListaNivelPorCampaña(SelectedCampañaId);

            //TBPremioModel obj = new TBPremioModel();

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();
            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");

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