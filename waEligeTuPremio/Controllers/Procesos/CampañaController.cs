using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using waEligeTuPremio.Data;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Procesos
{
    public class CampañaController : Controller
    {
        // GET: Campaña
        public ActionResult PremioPorCampaña()
        {

            TBCampañaModel obj = new TBCampañaModel();

            List<TBCampañaModel> ListaCampaña = DAOCampaña.SelectAll();

            obj.ListaCampaña = new SelectList(ListaCampaña, "intCampaña", "vchDescripcion");
           
         
            return View(obj);
        }

        [HttpPost]
        public ActionResult DescargaArchivo(TBCampañaModel CampañaModel)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();


              
                 dt= DAOCampaña.PremioPorCampaña(Convert.ToInt32( CampañaModel.SelectedCampañaId));

                  // dt = ToDataTableCSV(dt);
                //}
                ////wb.Worksheets.Add(dt, "Seguimiento");
                var ws = wb.Worksheets.Add(dt, "Premio Por Consultora");

                ws.Cells("A1:S1").Style.Fill.BackgroundColor = XLColor.Orange;

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", DateTime.Now.ToShortDateString() + ".xlsx");
                }
            }
        }

    }
}