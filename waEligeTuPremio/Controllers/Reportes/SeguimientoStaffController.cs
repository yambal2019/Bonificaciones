using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;

namespace waEligeTuPremio.Controllers.Reportes
{
    [Authorize]
    public class SeguimientoStaffController : Controller
    {
        // GET: SeguimientoStaff
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult SeguimientoStaffIndex()
        {
            using (var db = new DBPremioEntities())
            {
                List<SP_GetListaPlan> plan = db.Database.SqlQuery<SP_GetListaPlan>("GetListaPlan").ToList();
                ViewBag.Plan = ToSelectList(plan, "1");

                return View();
            }
        }

        [HttpPost]
        public ActionResult SeguimientoStaffIndex(SP_GetListaPlan plan)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();
                string nombPlan = "";
                //TRAEMOS TODAS LAS CNS QUE FINALIZARON SU PEDIDO SEGUN EL PLAN
                using (var db = new DBPremioEntities())
                {

                    if (plan.intCodigo == 0)
                    {
                        List<SP_GetListaPlan> Lplan = db.Database.SqlQuery<SP_GetListaPlan>("GetListaPlan").ToList();
                        ViewBag.Plan = ToSelectList(Lplan, "1");

                        return View();
                    }

                    nombPlan = db.Database.SqlQuery<string>("SELECT vchPlan FROM TPremio WHERE intCodigo = " + plan.intCodigo).FirstOrDefault();

                    List<SP_GetListaPedidoDirStaff> Lisatdo = db.Database.SqlQuery<SP_GetListaPedidoDirStaff>("GetListaPedidoDirStaff @vchPlan",
                        new SqlParameter("vchPlan", nombPlan)).ToList();

                    dt = ToDataTableCSV(Lisatdo);
                }
                //wb.Worksheets.Add(dt, "Seguimiento");
                var ws = wb.Worksheets.Add(dt, "Seguimiento");

                ws.Cells("A1:S1").Style.Fill.BackgroundColor = XLColor.Orange;

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Seguimiento_" + nombPlan + "_" +
                    DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

        [NonAction]
        public SelectList ToSelectList(List<SP_GetListaPlan> perfil, string SelectedID)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in perfil)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.vchPlan,
                    Value = item.intCodigo.ToString(),
                });
            }
            return new SelectList(list, "Value", "Text", SelectedID);
        }

        public DataTable ToDataTableCSV<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}