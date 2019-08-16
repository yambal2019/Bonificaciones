using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using waEligeTuPremio.Models;
using ClosedXML.Excel;
using System.Reflection;

namespace waEligeTuPremio.Controllers.Procesos
{
    [Authorize]
    public class PremiosController : Controller
    {
        // GET: Premios
        [OutputCache(Duration = 0, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult PremiosIndex()
        {
            using (var db = new DBPremioEntities())
            {
                return View(db.Database.SqlQuery<SP_GetPremio>("GetPremio").ToList());
            }
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
                            string filename = "Premios.xlsx";
                            string absolutePath = Server.MapPath("../Upload/" + filename);

                            if (System.IO.File.Exists(absolutePath))
                                System.IO.File.Delete(absolutePath);

                            archivo.SaveAs(Server.MapPath("../Upload/") + filename);

                            if (System.IO.File.Exists(absolutePath))
                            {
                                string rutaExcel = Server.MapPath("../Upload/" + filename);

                                DataTable dt = new DataTable("Premios");
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
                                                    if (worksheet.Cells[row, col].Value != null)
                                                        dr[x++] = worksheet.Cells[row, col].Value;
                                                    else
                                                        return Json("Campo vacío en la fila " + row + " columna " + col);

                                                    List<int> columnas = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 };
                                                    if (columnas.Contains(col))
                                                    {
                                                        int entero = 0;
                                                        bool sw = int.TryParse(worksheet.Cells[row, col].Value.ToString(), out entero);
                                                        if (!sw)
                                                            return Json("Debe ser un número en la fila " + row + " columna " + col);

                                                    }

                                                    //validamos que no se repita plan en la tabla premio
                                                    if (col == 1)
                                                    {
                                                        using (var db = new DBPremioEntities())
                                                        {
                                                            var sql = "SELECT COUNT(*) from TPremio WHERE vchPlan = '" + worksheet.Cells[row, col].Value.ToString() + "'";
                                                            int total = db.Database.SqlQuery<int>(sql).First();

                                                            if (total != 0)
                                                                return Json("El nombre del Plan " + worksheet.Cells[row, col].Value.ToString() + " ya existe.");

                                                        }
                                                    }

                                                }
                                                dt.Rows.Add(dr);
                                                //dr[x++] = int.Parse(HttpContext.User.Identity.Name);
                                            }
                                        }

                                    }
                                }

                                string errorbulkcopy;
                                bool resultadobulkcopy;
                                resultadobulkcopy = cBulk.CopiarDatosBulkPremios(dt, "TPremioTemp", out errorbulkcopy);
                                if (resultadobulkcopy)
                                {
                                    return Json("Status de Importacion: Se subio correctamente el archivo de Premios.");
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
        public ActionResult AgregarPremios()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    var sql = "SELECT COUNT(*) from TPremioTemp";
                    int total = db.Database.SqlQuery<int>(sql).First();

                    if (total == 0)
                        return Json("Debe subir primero el archivo Excel con la lista de premios.");

                    //if (productos.Count == 0)
                    //{
                    db.Database.ExecuteSqlCommand("InsertPremios @intCodigo",
                        new SqlParameter("intCodigo ", int.Parse(HttpContext.User.Identity.Name)));

                    return Json("Se actualizó los datos de Premios correctamente.");
                    //}
                    //else
                    //{
                    //    return Json("No se puede cargar una nueva lista de premios si ya existe una lista activa.");
                    //}
                }
            }
            catch (Exception)
            {
                return Json("No se puedo Realizar la Actualización de los Premios.");
                throw;
            }

        }

        [HttpPost]
        public ActionResult UploadImg()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
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

                            //string codSap = Request.Form["codSap"].ToString();
                            string intCodigo = Request.Form["intCodigo"].ToString();
                            using (var db = new DBPremioEntities())
                            {
                                //db.Database.ExecuteSqlCommand("UpdatePremioImagen @vchCodigoSAP, @imgImagen",
                                //new SqlParameter("vchCodigoSAP", codSap),
                                //new SqlParameter("imgImagen", byteImg));

                                db.Database.ExecuteSqlCommand("UpdatePremioImagenNew @vchNombre, @vchExtencion, @vchImagen, @intCodigo",
                                new SqlParameter("vchNombre", archivo.FileName.Substring(archivo.FileName.LastIndexOf(@"\") + 1)),
                                new SqlParameter("vchExtencion", archivo.ContentType),
                                new SqlParameter("vchImagen", byteImg),
                                new SqlParameter("intCodigo", intCodigo)
                                );

                            }

                            return Json("Status de Importacion: Se subio correctamente la imagen.");
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
                return Json("Seleccione una Imagen." + Request.Form["codSap"].ToString());
            }
        }

        public ActionResult CreatePremio()
        {
            ViewBag.Title = "Crear Premio";

            return PartialView();
        }
        [HttpPost]
        public ActionResult CreatePremio(SP_GetPremio pre)
        {

            using (var db = new DBPremioEntities())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("InsertPremiosUnitarioNew @intCodigoSAP, @intCodigoCorto, @intOrden," +
                                                  " @vchTitulo, @vchDescripcion, @smintStock, @smintPuntos, @intCodigo , @intNivel, @bitInicial", //@smintPuntos,
                    new SqlParameter("intCodigoSAP", pre.intCodigoSAP),
                    new SqlParameter("intCodigoCorto", pre.intCodigoCorto),
                    new SqlParameter("intOrden", pre.intOrden),
                    new SqlParameter("vchTitulo", pre.vchTitulo),
                    new SqlParameter("vchDescripcion", pre.vchDescripcion),
                    new SqlParameter("smintStock", pre.smintStock),
                    new SqlParameter("smintPuntos", pre.smintPuntos),
                    new SqlParameter("intCodigo", int.Parse(HttpContext.User.Identity.Name)),
                     new SqlParameter("intNivel", pre.intNivel),
                     new SqlParameter("bitInicial", pre.bitInicial)

                    );


                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                    throw;
                }
            }

        }
        public ActionResult EditImg(int id = 0)
        {

            using (var db = new DBPremioEntities())
            {
                SP_GetPremio premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioSAP @vchCodigoSAP",
                    new SqlParameter("vchCodigoSAP", id)).FirstOrDefault();

                ViewBag.Title = "Editar Producto";
                return PartialView(premio);
            }
        }

        [HttpPost]
        public ActionResult EditImg(SP_GetPremio pre)
        {
            using (var db = new DBPremioEntities())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("UpdatePremio @intCodigoSAP, @intCodigoCorto, @intOrden, @vchDescripcion, @smintStock, @smintPuntos, @bitActivo, @vchTitulo",
                    new SqlParameter("intCodigoSAP", pre.intCodigoSAP),
                    new SqlParameter("intCodigoCorto", pre.intCodigoCorto),
                    new SqlParameter("intOrden", pre.intOrden),
                    new SqlParameter("vchDescripcion", pre.vchDescripcion),
                    new SqlParameter("smintStock", pre.smintStock),
                    new SqlParameter("smintPuntos", 1), //pre.smintPuntos
                    new SqlParameter("bitActivo", pre.bitActivo),
                    new SqlParameter("vchTitulo", pre.vchTitulo));

                    return Json(new { success = true });
                }
                catch (Exception)
                {
                    return Json(new { success = false });
                    throw;
                }
            }
        }

        [HttpPost]
        public ActionResult EliminarPlan()
        {
            try
            {
                using (var db = new DBPremioEntities())
                {
                    db.Database.ExecuteSqlCommand("DeletePremio");

                    return Json("Se eliminó correctamente el Plan de Ganamas.");
                }
            }
            catch (Exception)
            {
                return Json("No se pudo elimnar el Plan de Ganamas.");
                throw;
            }

        }


        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult CodigoSAPValido(int intCodigoSAP)
        {
            SP_GetPremio premio = new SP_GetPremio();
            using (var db = new DBPremioEntities())
            {
                premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioSAPExiste @vchCodigoSAP",
                    new SqlParameter("vchCodigoSAP", intCodigoSAP)).FirstOrDefault();
            }
            var validUserName = true;
            if (premio != null)
                validUserName = premio.intCodigoSAP.ToString().Length == 0;

            return Json(validUserName, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult CodigoCortoValido(int intCodigoCorto)
        {
            SP_GetPremio premio = new SP_GetPremio();
            using (var db = new DBPremioEntities())
            {
                premio = db.Database.SqlQuery<SP_GetPremio>("GetPremioCortoExiste @intCodigoCorto",
                    new SqlParameter("intCodigoCorto", intCodigoCorto)).FirstOrDefault();
            }
            var validUserName = true;
            if (premio != null)
                validUserName = premio.intCodigoCorto.ToString().Length == 0;

            return Json(validUserName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadFile()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();
                using (var db = new DBPremioEntities())
                {
                    List<SP_GetFormatoPremio> lPremio = db.Database.SqlQuery<SP_GetFormatoPremio>("GetFormatoPremio").ToList();

                    dt = ToDataTableCSV(lPremio);
                }


                wb.Worksheets.Add(dt, "Premios");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Formato Premio_" +
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

    }
}