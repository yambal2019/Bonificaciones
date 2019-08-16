using ClosedXML.Excel;
using OfficeOpenXml;
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

namespace waEligeTuPremio.Controllers.Parametros
{
    [Authorize]
    public class CalendarioController : Controller
    {
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        // GET: Calendario
        public ActionResult CalendarioIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase archivo = files[0];

                    if (archivo == null || archivo.ContentLength == 0)
                    {
                        return Json("Seleccione un archivo Excel.");
                    }
                    else
                    {

                        if (archivo.FileName.EndsWith("xls") || archivo.FileName.EndsWith("xlsx"))
                        {
                            string filename = "Calendario.xlsx";
                            string absolutePath = Server.MapPath("../Upload/" + filename);

                            if (System.IO.File.Exists(absolutePath))
                                System.IO.File.Delete(absolutePath);

                            archivo.SaveAs(Server.MapPath("../Upload/") + filename);

                            if (System.IO.File.Exists(absolutePath))
                            {
                                string rutaExcel = Server.MapPath("../Upload/" + filename);

                                DataTable dt = new DataTable("Calendario");
                                var existingFile = new FileInfo(rutaExcel);
                                using (var package = new ExcelPackage(existingFile))
                                {
                                    ExcelWorkbook workBook = package.Workbook;
                                    if (workBook != null)
                                    {
                                        if (workBook.Worksheets.Count > 0)
                                        {
                                            ExcelWorksheet worksheet = workBook.Worksheets.First();
                                            ExcelCellAddress startCell = worksheet.Dimension.Start;
                                            ExcelCellAddress endCell = worksheet.Dimension.End;

                                            for (int col = startCell.Column; col <= endCell.Column; col++)
                                            {
                                                object col1Header1 = worksheet.Cells[1, col].Value; //1
                                                dt.Columns.Add("" + col1Header1 + "");
                                            }
                                            for (int row = 2; row <= endCell.Row; row++)
                                            {
                                                DataRow dr = dt.NewRow();
                                                int x = 0;
                                                for (int col = startCell.Column; col <= endCell.Column; col++)
                                                {
                                                    dr[x++] = worksheet.Cells[row, col].Value;
                                                }
                                                dt.Rows.Add(dr);
                                            }
                                        }

                                    }
                                }

                                string errorbulkcopy;
                                bool resultadobulkcopy;
                                resultadobulkcopy = cBulk.CopiarDatosBulkCalendario(dt, "TCalendarioTemp", out errorbulkcopy);
                                if (resultadobulkcopy)
                                {
                                    return Json("Status de Importacion: Se subio correctamente el archivo de Calendario.");
                                }
                                else
                                {
                                    return Json("Status de Importacion: Hubo un error al Importar! Verifique archivo de Excel." + errorbulkcopy);
                                }
                            }
                            else
                            {
                                return Json("El Archivo no Existe.");
                            }
                        }
                        else
                        {
                            return Json("Tipo incorrecto de Archivo.");
                        }
                    }



                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("Seleccione un Archivo Excel.");
            }
        }

        [HttpPost]
        public ActionResult AgregarCalendario()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    db.Database.ExecuteSqlCommand("AgregarCalendario @intCodUsuario",
                        new SqlParameter("intCodUsuario", int.Parse(HttpContext.User.Identity.Name)));

                    return Json("Se actualizó los datos de Calendario correctamente.");
                }
            }
            catch (Exception)
            {
                return Json("No se puedo Realizar la Actualización de los Datos de Calendario.");
                throw;
            }

        }

        public FileResult downloadFile()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();
                using (var db = new DBPremioEntities())
                {
                    List<SP_Calendario> lCalendario = db.Database.SqlQuery<SP_Calendario>("GetCalendario").ToList();

                    dt = ToDataTable(lCalendario);
                }

                wb.Worksheets.Add(dt, "Calendario");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calendario_" +
                    DateTime.Now.ToString() + ".xlsx");
                }

            }

        }

        public DataTable ToDataTable<T>(List<T> items)
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