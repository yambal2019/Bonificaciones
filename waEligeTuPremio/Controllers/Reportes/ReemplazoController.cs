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
    public class ReemplazoController : Controller
    {
        // GET: Reemplazo
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ReemplazoIndex()
        {
            using (var db = new DBPremioEntities())
            {
                List<SP_GetListaPlan> plan = db.Database.SqlQuery<SP_GetListaPlan>("GetListaPlan").ToList();
                ViewBag.Plan = ToSelectList(plan,"1");

                return View();
            }
        }

        [HttpPost]
        public ActionResult ReemplazoIndex(SP_GetListaPlan plan)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {

                DataTable dt = new DataTable();
                //TRAEMOS TODAS LAS CNS QUE FINALIZARON SU PEDIDO SEGUN EL PLAN
                using (var db = new DBPremioEntities())
                {

                    if (plan.intCodigo == 0)
                    {
                        List<SP_GetListaPlan> Lplan = db.Database.SqlQuery<SP_GetListaPlan>("GetListaPlan").ToList();
                        ViewBag.Plan = ToSelectList(Lplan, "1");

                        return View();
                    }

                    string nombPlan = db.Database.SqlQuery<string>("SELECT vchPlan FROM TPremio WHERE intCodigo = " + plan.intCodigo).FirstOrDefault();

                    List<SP_GetReemplazoPremio> lCalendario = db.Database.SqlQuery<SP_GetReemplazoPremio>("GetReemplazoPremio @vchPlan, @vchTodos",
                        new SqlParameter("vchPlan", nombPlan),
                        new SqlParameter("vchTodos", "SI")).ToList();

                    dt = ToDataTableCSV(lCalendario);
                }
                wb.Worksheets.Add(dt, "ReemplazoTodos");

                //TRAEMOS TODAS LAS CNS QUE FINALIZARON SU PEDIDO SEGUN EL PLAN Y QUE EL ESTADO SEA NULL
                using (var db = new DBPremioEntities())
                {

                    if (plan.intCodigo == 0)
                    {
                        List<SP_GetListaPlan> Lplan = db.Database.SqlQuery<SP_GetListaPlan>("GetListaPlan").ToList();
                        ViewBag.Plan = ToSelectList(Lplan, "1");

                        return View();
                    }

                    string nombPlan = db.Database.SqlQuery<string>("SELECT vchPlan FROM TPremio WHERE intCodigo = " + plan.intCodigo).FirstOrDefault();

                    List<SP_GetReemplazoPremio> lCalendario = db.Database.SqlQuery<SP_GetReemplazoPremio>("GetReemplazoPremio @vchPlan, @vchTodos",
                        new SqlParameter("vchPlan", nombPlan),
                        new SqlParameter("vchTodos", "NO")).ToList();

                    dt = ToDataTableCSV(lCalendario);

                    //ACTULIZAMOS EL ESTADO DEL PEDIDO A DESCARGADO
                    db.Database.ExecuteSqlCommand("UPDATE TPedido SET vchEstadoDescarga = 'SI' WHERE TPedido.vchPlan = '" + nombPlan + "' AND vchEstadoDescarga is NULL");

                }
                wb.Worksheets.Add(dt, "ReemplazoNuevos");


                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReemplazoPremio_" +
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

        public ActionResult DownloadFile()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();
                using (var db = new DBPremioEntities())
                {
                    List<SP_GetReemplazoPremio> lCalendario = db.Database.SqlQuery<SP_GetReemplazoPremio>("GetReemplazoPremio").ToList();

                    dt = ToDataTableCSV(lCalendario);
                }


                //MemoryStream stream = GetCSV(dt);

                //var filename = "ReemplazoPremio" + DateTime.Now.ToString() + ".csv";
                //var contenttype = "text/csv";
                //Response.Clear();
                //Response.ContentType = contenttype;
                //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.BinaryWrite(stream.ToArray());
                //Response.End();

                //return Json(null);

                wb.Worksheets.Add(dt, "Reemplazo");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReemplazoPremio_" +
                    DateTime.Now.ToString() + ".xlsx");
                }

            }

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

        public static MemoryStream GetCSV(DataTable data)
        {
            string[] fieldsToExpose = new string[data.Columns.Count];
            for (int i = 0; i < data.Columns.Count; i++)
            {
                fieldsToExpose[i] = data.Columns[i].ColumnName;
            }

            return GetCSV(fieldsToExpose, data);
        }

        public static MemoryStream GetCSV(string[] fieldsToExpose, DataTable data)
        {
            MemoryStream stream = new MemoryStream();
            using (var writer = new StreamWriter(stream))
            {
                //for (int i = 0; i < fieldsToExpose.Length; i++)
                //{
                //    if (i != 0) { writer.Write(","); }
                //    writer.Write("\"");
                //    writer.Write(fieldsToExpose[i].Replace("\"", "\"\""));
                //    writer.Write("\"");
                //}
                //writer.Write("\n");

                foreach (DataRow row in data.Rows)
                {
                    for (int i = 0; i < fieldsToExpose.Length; i++)
                    {
                        if (i != 0)
                        {
                            writer.Write(",");
                        }
                        //writer.Write("\"");
                        writer.Write(row[fieldsToExpose[i]].ToString().Replace("\"", "\"\""));
                        //writer.Write("\"");
                    }

                    writer.Write("\n");
                }
            }

            return stream;
        }

    }
}